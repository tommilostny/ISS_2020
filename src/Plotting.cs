using NAudio.Wave;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src
{
    public static class Plotting
    {
        private const int Fs = 16000;           //16000 = 1s (Fs = 16kHz)

        private static DataPoint[] LoadAudioSamples(string filePath, int samplesCount = Fs)
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

        public static PlotModel PlotWavFile(PlotData plotData, double seconds = 1.0)
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
                Maximum = seconds * Fs,
                Minimum = 0,
                Position = AxisPosition.Bottom
            };
            var y_axis = new LinearAxis
            {
                Maximum = 1100,
                Minimum = -1100,
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
            stemSeries.Points.AddRange(LoadAudioSamples(plotData.FullFilePath, (int)(seconds * Fs)));
            pm.Series.Add(stemSeries);

            return pm;
        }

        private static async Task ZoomPlotAsync(PlotData plot, int x1, int x2)
        {
            await Task.Run(() => plot.XAxis.Zoom(x1, x2));
        }

        private static async Task<string> ZoomLabelStringAsync(int minTBValue, int maxTBValue)
        {
            return await Task.Run(() => $"from {(double)minTBValue / 100}s to {(double)maxTBValue / 100}s");
        }

        public static async Task<string> ScrollZoomUpdateAsync(TrackBar min, TrackBar max, SplitContainer plotsSplitContainer, PlotData plot1, PlotData plot2)
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
