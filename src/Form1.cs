using NAudio.Wave;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.Threading.Tasks;

namespace src
{
    public partial class Form1 : Form
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private string SelectedFile => $@"..\..\..\..\audio\{comboBox1.Items[comboBox1.SelectedIndex]}.wav";

        private record PlotData
        {
            public string PlotTitle { get; init; }
            public string FileName { get; init; }
            public string FullFilePath => $@"..\..\..\..\audio\{FileName}";
            public Axis XAxis { get; set; }
        }

        private readonly PlotData maskOffTone;  //Mask on data for plotting
        private readonly PlotData maskOnTone;   //Mask off data for plotting
        private const int Fs = 16000;           //16000 = 1s (Fs = 16kHz)

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

            maskOffTone = new PlotData
            {
                PlotTitle = "Mask off tone",
                FileName = "maskoff_tone.wav"
            };
            maskOnTone = new PlotData
            {
                PlotTitle = "Mask on tone",
                FileName = "maskon_tone.wav"
            };

            plotViewMaskOffTone.Model = PlotWavFile(maskOffTone);
            plotViewMaskOnTone.Model = PlotWavFile(maskOnTone);
        }

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

        private static PlotModel PlotWavFile(PlotData plotData, double seconds = 1.0)
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

        private void OnButtonStopClick(object sender, EventArgs e)
        {
            outputDevice?.Stop();
        }

        private void OnButtonPlayClick(object sender, EventArgs e)
        {
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(SelectedFile);
                outputDevice.Init(audioFile);
            }
            outputDevice.Play();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
        }

        private static async Task<bool> ZoomPlotAsync(PlotData plot, int x1, int x2)
        {
            await Task.Run(() => plot.XAxis.Zoom(x1, x2));
            return true;
        }

        private async Task ScrollZoomUpdateAsync()
        {
            int x1 = minTrackBar.Value * 160;
            int x2 = maxTrackBar.Value * 160;

            Task<bool> task1 = ZoomPlotAsync(maskOffTone, x1, x2);
            Task<bool> task2 = ZoomPlotAsync(maskOnTone, x1, x2);

            label3.Text = $"From {(double)minTrackBar.Value / 100}s to {(double)maxTrackBar.Value / 100}s.";

            if (await task1 && await task2)
                splitContainer1.Refresh();
        }

        private async void MaxTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (maxTrackBar.Value <= minTrackBar.Value)
            {
                maxTrackBar.Value++;
            }
            await ScrollZoomUpdateAsync();
        }

        private async void MinTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (minTrackBar.Value >= maxTrackBar.Value)
            {
                minTrackBar.Value--;
            }
            await ScrollZoomUpdateAsync();
        }

        private void ZoomPlusButton_Click(object sender, EventArgs e)
        {
            if (maxTrackBar.Value - minTrackBar.Value > 1)
            {
                if (minTrackBar.Value < 99)
                    minTrackBar.Value++;

                if (maxTrackBar.Value > 1)
                    maxTrackBar.Value--;
            }
        }

        private void ZoomMinusButton_Click(object sender, EventArgs e)
        {
            if (minTrackBar.Value > 0)
                minTrackBar.Value--;

            if (maxTrackBar.Value < 100)
                maxTrackBar.Value++;
        }
    }
}
