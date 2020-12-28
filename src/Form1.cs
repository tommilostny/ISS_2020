﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectISS
{
    public partial class Form1 : Form
    {
        private readonly Playback filePlayer;

        private readonly SamplesData maskOffTone;  //Mask on data for plotting
        private readonly SamplesData maskOnTone;   //Mask off data for plotting

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            filePlayer = new Playback(comboBox1);

            maskOffTone = new SamplesData
            {
                PlotTitle = "Mask off tone",
                FileName = "maskoff_tone.wav",
                Seconds = 1.0
            };
            maskOnTone = new SamplesData
            {
                PlotTitle = "Mask on tone",
                FileName = "maskon_tone.wav",
                Seconds = 1.0
            };

            buttonPlay.Click += filePlayer.OnButtonPlayClick;
            buttonStop.Click += filePlayer.OnButtonStopClick;
            comboBox1.SelectedIndexChanged += filePlayer.OnButtonStopClick;

            Plotting.MeanNormalizeAsync(maskOffTone, maskOnTone);

            plotViewMaskOffTone.Model = Plotting.PlotWavFile(maskOffTone);
            plotViewMaskOnTone.Model = Plotting.PlotWavFile(maskOnTone);

            Plotting.LoadFramesAsync(maskOffTone, maskOnTone, 20);
        }

        private async void MaxTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (maxTrackBar.Value <= minTrackBar.Value)
            {
                maxTrackBar.Value++;
            }
            intervalLabel.Text = await ScrollZoomUpdateAsync();
        }

        private async void MinTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (minTrackBar.Value >= maxTrackBar.Value)
            {
                minTrackBar.Value--;
            }
            intervalLabel.Text = await ScrollZoomUpdateAsync();
        }

        private static async Task ZoomPlotAsync(SamplesData plot, double x1, double x2)
        {
            await Task.Run(() => plot.XAxis.Zoom(x1, x2));
        }

        private async Task<string> ScrollZoomUpdateAsync()
        {
            double x1 = minTrackBar.Value / 100.0;
            double x2 = maxTrackBar.Value / 100.0;

            var zoomTask1 = ZoomPlotAsync(maskOnTone, x1, x2);
            var zoomTask2 = ZoomPlotAsync(maskOffTone, x1, x2);

            await Task.WhenAll(zoomTask1, zoomTask2);
            plotsSplitContainer.Refresh();

            return $"from {x1}s to {x2}s";
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

        private void ButtonFrames_Click(object sender, EventArgs e)
        {
            var frameForm1 = new FrameForm(maskOffTone)
            {
                Text = $"{maskOffTone.PlotTitle} frames",
                Left = 40,
                Top = 200
            };

            var frameForm2 = new FrameForm(maskOnTone)
            {
                Text = $"{maskOnTone.PlotTitle} frames",
                Left = 955,
                Top = 200
            };

            frameForm1.Show();
            frameForm2.Show();
        }
    }
}
