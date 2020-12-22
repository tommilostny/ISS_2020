using NAudio.Wave;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectISS
{
    public static class Plotting
    {
        public const int Fs = 16000;           //16000 = 1s (Fs = 16kHz)

        public static DataPoint[] LoadAudioSamples(string filePath, int samplesCount = Fs)
        {
            var datapoints = new DataPoint[samplesCount];
            var samples = new short[samplesCount];

            //Read WAV file into array of 16-bit integers
            using (var waveFileReader = new WaveFileReader(filePath))
            {
                var buffer = new byte[samplesCount * 2];
                waveFileReader.Read(buffer, 0, buffer.Length);
                Buffer.BlockCopy(buffer, 0, samples, 0, samplesCount * 2);
            }

            //Convert read array to datapoints for OxyPlot
            for (int i = 0; i < samplesCount; i++)
            {
                datapoints[i] = new DataPoint(i, samples[i]);
            }
            return datapoints;
        }

        private static async Task NormalizeAsync(DataPoint[] points, double max)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new(points[i].X, points[i].Y / max);
                }
            });
        }

        private static async Task<double> GetMaxAsync(SamplesData points)
        {
            return await Task.Run(() => points.DataPoints.Max(point => Math.Abs(point.Y)));
        }

        public static async void NormalizeDataPointsAsync(SamplesData p1, SamplesData p2)
        {
            var mtask1 = GetMaxAsync(p1);
            var mtask2 = GetMaxAsync(p2);

            double m1 = await mtask1;
            double m2 = await mtask2;
            double max = m1 > m2 ? m1 : m2;

            var task1 = NormalizeAsync(p1.DataPoints, max);
            var task2 = NormalizeAsync(p2.DataPoints, max);

            p1.IsNormalized = p2.IsNormalized = true;
            await Task.WhenAll(task1, task2);
        }

        public static PlotModel PlotWavFile(SamplesData plotData)
        {
            //OxyPlot model setup
            var pm = new PlotModel
            {
                Title = plotData.PlotTitle,
                Subtitle = plotData.FileName,
                Background = OxyColors.White,
            };

            //XY axes setup
            plotData.XAxis = new LinearAxis
            {
                Maximum = plotData.Seconds * Fs,
                Minimum = 0,
                Position = AxisPosition.Bottom
            };
            var y_axis = new LinearAxis
            {
                Maximum = plotData.IsNormalized ? 1 : 1100,
                Minimum = plotData.IsNormalized ? -1 : -1100,
                Position = AxisPosition.Left,
                IsZoomEnabled = false
            };
            pm.Axes.Add(plotData.XAxis);
            pm.Axes.Add(y_axis);

            //Setup stem
            var stemSeries = new StemSeries
            {
                MarkerStroke = OxyColors.Green,
                MarkerType = MarkerType.Circle
            };
            stemSeries.Points.AddRange(plotData.DataPoints);
            pm.Series.Add(stemSeries);

            return pm;
        }

        private static async Task ZoomPlotAsync(SamplesData plot, int x1, int x2)
        {
            await Task.Run(() => plot.XAxis.Zoom(x1, x2));
        }

        private static async Task<string> ZoomLabelStringAsync(int minTBValue, int maxTBValue)
        {
            return await Task.Run(() => $"from {(double)minTBValue / 100}s to {(double)maxTBValue / 100}s");
        }

        public static async Task<string> ScrollZoomUpdateAsync(TrackBar min, TrackBar max, SplitContainer plotsSplitContainer, SamplesData plot1, SamplesData plot2)
        {
            int x1 = min.Value * 160;
            int x2 = max.Value * 160;

            var zoomTask1 = ZoomPlotAsync(plot1, x1, x2);
            var zoomTask2 = ZoomPlotAsync(plot2, x1, x2);
            var labelTask = ZoomLabelStringAsync(min.Value, max.Value);

            await Task.WhenAll(zoomTask1, zoomTask2);
            plotsSplitContainer.Refresh();

            return await labelTask;
        }
    }
}
