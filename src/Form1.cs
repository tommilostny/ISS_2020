﻿using NAudio.Wave;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace src
{
    public partial class Form1 : Form
    {
        private readonly Playback filePlayer;

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
            filePlayer = new Playback(comboBox1);

            buttonPlay.Click += filePlayer.OnButtonPlayClick;
            buttonStop.Click += filePlayer.OnButtonStopClick;
            comboBox1.SelectedIndexChanged += filePlayer.OnButtonStopClick;

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

        private static async Task ZoomPlotAsync(PlotData plot, int x1, int x2)
        {
            await Task.Run(() => plot.XAxis.Zoom(x1, x2));
        }

        private static async Task<string> ZoomLabelStringAsync(int minTBValue, int maxTBValue)
        {
            return await Task.Run(() => $"from {(double)minTBValue / 100}s to {(double)maxTBValue / 100}s");
        }

        private async Task ScrollZoomUpdateAsync()
        {
            int x1 = minTrackBar.Value * 160;
            int x2 = maxTrackBar.Value * 160;

            var zoomTask1 = ZoomPlotAsync(maskOffTone, x1, x2);
            var zoomTask2 = ZoomPlotAsync(maskOnTone, x1, x2);
            var labelTask = ZoomLabelStringAsync(minTrackBar.Value, maxTrackBar.Value);

            await Task.WhenAll(zoomTask1, zoomTask2);
            plotsSplitContainer.Refresh();

            intervalLabel.Text = await labelTask;
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

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            if (maxTrackBar.Value - minTrackBar.Value > 1)
            {
                if (minTrackBar.Value < 99)
                    minTrackBar.Value++;

                if (maxTrackBar.Value > 1)
                    maxTrackBar.Value--;
            }
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            if (minTrackBar.Value > 0)
                minTrackBar.Value--;

            if (maxTrackBar.Value < 100)
                maxTrackBar.Value++;
        }

        private void MoveForwardButton_Click(object sender, EventArgs e)
        {
            if (maxTrackBar.Value < 100)
            {
                maxTrackBar.Value++;
                minTrackBar.Value++;
            }
        }

        private void MoveBackwardButton_Click(object sender, EventArgs e)
        {
            if (minTrackBar.Value > 0)
            {
                minTrackBar.Value--;
                maxTrackBar.Value--;
            }
        }
    }
}
