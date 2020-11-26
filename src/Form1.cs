﻿using System;
using System.Windows.Forms;

namespace src
{
    public partial class Form1 : Form
    {
        private readonly Playback filePlayer;

        private readonly PlotData maskOffTone;  //Mask on data for plotting
        private readonly PlotData maskOnTone;   //Mask off data for plotting

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            filePlayer = new Playback(comboBox1);

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

            buttonPlay.Click += filePlayer.OnButtonPlayClick;
            buttonStop.Click += filePlayer.OnButtonStopClick;
            comboBox1.SelectedIndexChanged += filePlayer.OnButtonStopClick;

            plotViewMaskOffTone.Model = Plotting.PlotWavFile(maskOffTone);
            plotViewMaskOnTone.Model = Plotting.PlotWavFile(maskOnTone);
        }

        private async void MaxTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (maxTrackBar.Value <= minTrackBar.Value)
            {
                maxTrackBar.Value++;
            }
            intervalLabel.Text = await Plotting.ScrollZoomUpdateAsync(minTrackBar, maxTrackBar, plotsSplitContainer, maskOffTone, maskOnTone);
        }

        private async void MinTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (minTrackBar.Value >= maxTrackBar.Value)
            {
                minTrackBar.Value--;
            }
            intervalLabel.Text = await Plotting.ScrollZoomUpdateAsync(minTrackBar, maxTrackBar, plotsSplitContainer, maskOffTone, maskOnTone);
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
