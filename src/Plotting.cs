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
                //datapoint.X: time
                //datapoint.Y: sample value
                datapoints[i] = new DataPoint((double)i / Fs, samples[i]);
            }
            return datapoints;
        }

        private static async Task MeanNormalizeRoutineAsync(DataPoint[] points)
        {
            await Task.Run(() =>
            {
                //ustřednění (x -= np.mean(x)), ekvivalent Average
                double mean = points.Average(point => point.Y);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new(points[i].X, points[i].Y - mean);
                }

                //normalizace (x /= np.abs(x).max())
                double max = points.Max(point => Math.Abs(point.Y));
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new(points[i].X, points[i].Y / max);
                }
            });
        }

        public static async void MeanNormalizeAsync(SamplesData x1, SamplesData x2)
        {
            var task1 = MeanNormalizeRoutineAsync(x1.DataPoints);
            var task2 = MeanNormalizeRoutineAsync(x2.DataPoints);

            x1.IsNormalized = x2.IsNormalized = true;
            await Task.WhenAll(task1, task2);
        }


        private static async Task FramesRoutineAsync(SamplesData x, double frameLengthMs)
        {
            await Task.Run(() =>
            {
                //vzorec pro výpočet velikosti rámce ve vzorcích
                int frameLength = (int)(frameLengthMs / 1000 * Fs);

                for (int i = 0; i < x.DataPoints.Length - frameLength / 2; i += frameLength / 2)
                {
                   x.Frames.Add(new Frame(x.DataPoints, i, frameLength));
                }
            });
        }

        public static async void LoadFramesAsync(SamplesData x1, SamplesData x2, int frameLengthMs)
        {
            var task1 = FramesRoutineAsync(x1, frameLengthMs);
            var task2 = FramesRoutineAsync(x2, frameLengthMs);
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
                Maximum = plotData.Seconds,
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
            var lineSeries = new LineSeries
            {
                Color = OxyColors.DodgerBlue
            };
            lineSeries.Points.AddRange(plotData.DataPoints);
            pm.Series.Add(lineSeries);

            return pm;
        }

        private static async Task ZoomPlotAsync(SamplesData plot, double x1, double x2)
        {
            await Task.Run(() => plot.XAxis.Zoom(x1, x2));
        }

        public static async Task<string> ScrollZoomUpdateAsync(TrackBar min, TrackBar max, SplitContainer plotsSplitContainer, SamplesData plot1, SamplesData plot2)
        {
            double x1 = min.Value / 100.0;
            double x2 = max.Value / 100.0;

            var zoomTask1 = ZoomPlotAsync(plot1, x1, x2);
            var zoomTask2 = ZoomPlotAsync(plot2, x1, x2);

            await Task.WhenAll(zoomTask1, zoomTask2);
            plotsSplitContainer.Refresh();

            return $"from {x1}s to {x2}s";
        }
    }
}
