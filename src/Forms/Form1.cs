using ProjectISS.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectISS
{
    public partial class Form1 : Form
    {
        private readonly Playback filePlayer;

        private SamplesData maskOff;  //Mask on data for plotting
        private SamplesData maskOn;   //Mask off data for plotting

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            filePlayer = new Playback(comboBox1);

            radioButton1.Checked = true;

            buttonPlay.Click += filePlayer.OnButtonPlayClick;
            buttonStop.Click += filePlayer.OnButtonStopClick;
            comboBox1.SelectedIndexChanged += filePlayer.OnButtonStopClick;
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

            var zoomTask1 = ZoomPlotAsync(maskOn, x1, x2);
            var zoomTask2 = ZoomPlotAsync(maskOff, x1, x2);

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

        private void ShowFramesDialogs()
        {
            var frameForm1 = new FrameForm(maskOff, false)
            {
                Text = $"{maskOff.PlotTitle} frames",
                Left = 5,
                Top = 20
            };

            var frameForm2 = new FrameForm(maskOn, false)
            {
                Text = $"{maskOn.PlotTitle} frames",
                Left = 935,
                Top = 20
            };

            frameForm1.Show();
            frameForm2.Show();
        }

        private void ButtonFrames_Click(object sender, EventArgs e)
        {
            ShowFramesDialogs();
        }

        private void ShowAutocorrelationsDialogs()
        {
            var frameForm1 = new FrameForm(maskOff, true)
            {
                Text = $"{maskOff.PlotTitle} frames autocorrelation",
                Left = 5,
                Top = 500
            };

            var frameForm2 = new FrameForm(maskOn, true)
            {
                Text = $"{maskOn.PlotTitle} frames autocorrelation",
                Left = 935,
                Top = 500
            };

            frameForm1.Show();
            frameForm2.Show();
            new F0Form(maskOff, maskOn).Show();
        }

        private async void ButtonCenterClipping_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;

            var clipTask1 = SharedFuncs.CenterClipping(maskOn);
            var clipTask2 = SharedFuncs.CenterClipping(maskOff);
            await Task.WhenAll(clipTask1, clipTask2);
            ShowFramesDialogs();

            var autoTask1 = SharedFuncs.Autocorrelation(maskOn);
            var autoTask2 = SharedFuncs.Autocorrelation(maskOff);
            await Task.WhenAll(autoTask1, autoTask2);
            ShowAutocorrelationsDialogs();
        }

        private async void ButtonDFT_Click(object sender, EventArgs e)
        {
            var task1 = SharedFuncs.DFT(maskOff);
            var task2 = SharedFuncs.DFT(maskOn);
            await Task.WhenAll(task1, task2);

            var specform1 = new SpectrogramForm(maskOff);
            var specform2 = new SpectrogramForm(maskOn);
            specform1.Show();
            specform2.Show();

            var freqCharPoints = SharedFuncs.FrequencyChar(maskOff, maskOn);
            new FreqCharForm(freqCharPoints, "Frekvenční charakteristika").Show();

            var idftPoints = SharedFuncs.IDFT(freqCharPoints);
            new FreqCharForm(idftPoints, "Impulsní odezva roušky").Show();
        }

        private async Task SetSourceTone()
        {
            maskOff = new SamplesData
            {
                PlotTitle = "Tón bez roušky",
                FileName = "maskoff_tone.wav",
                Seconds = 1.01
            };
            maskOn = new SamplesData
            {
                PlotTitle = "Tón s rouškou",
                FileName = "maskon_tone.wav",
                Seconds = 1.01
            };
            await PlotsSetup();
        }

        private async Task SetSourceSentence()
        {
            maskOff = new SamplesData
            {
                PlotTitle = "Věta bez roušky",
                FileName = "maskoff_sentence.wav",
                Seconds = 1.01
            };
            maskOn = new SamplesData
            {
                PlotTitle = "Věta s rouškou",
                FileName = "maskon_sentence.wav",
                Seconds = 1.01
            };
            await PlotsSetup();
        }

        private async Task PlotsSetup()
        {
            await SharedFuncs.MeanNormalize(maskOff, maskOn);

            plotViewMaskOffTone.Model = SharedFuncs.PlotWavFile(maskOff);
            plotViewMaskOnTone.Model = SharedFuncs.PlotWavFile(maskOn);

            await SharedFuncs.LoadFrames(maskOff, maskOn);
        }

        private async void RadioButtonTone_CheckedChanged(object sender, EventArgs e)
        {
            await SetSourceTone();
        }

        private async void RadioButtonSentence_CheckedChanged(object sender, EventArgs e)
        {
            await SetSourceSentence();
        }
    }
}
