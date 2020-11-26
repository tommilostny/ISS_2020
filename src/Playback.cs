using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace src
{
    public class Playback
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private readonly ComboBox comboBox1;
        private string SelectedFile => $@"..\..\..\..\audio\{comboBox1.Items[comboBox1.SelectedIndex]}.wav";

        public Playback(ComboBox playFileSelectCB)
        {
            comboBox1 = playFileSelectCB;
        }

        public void OnButtonStopClick(object sender, EventArgs e)
        {
            outputDevice?.Stop();
        }

        public void OnButtonPlayClick(object sender, EventArgs e)
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
