using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src
{
    public partial class Form1 : Form
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        private string SelectedFile => $@"..\..\..\..\audio\{comboBox1.Items[comboBox1.SelectedIndex]}.wav";

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
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
    }
}
