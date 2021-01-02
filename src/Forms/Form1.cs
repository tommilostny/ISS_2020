using System;
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
                PlotTitle = "Tón bez roušky",
                FileName = "maskoff_tone.wav",
                Seconds = 1.01
            };
            maskOnTone = new SamplesData
            {
                PlotTitle = "Tón s rouškou",
                FileName = "maskon_tone.wav",
                Seconds = 1.01
            };

            buttonPlay.Click += filePlayer.OnButtonPlayClick;
            buttonStop.Click += filePlayer.OnButtonStopClick;
            comboBox1.SelectedIndexChanged += filePlayer.OnButtonStopClick;

            SharedFuncs.MeanNormalizeAsync(maskOffTone, maskOnTone);

            plotViewMaskOffTone.Model = SharedFuncs.PlotWavFile(maskOffTone);
            plotViewMaskOnTone.Model = SharedFuncs.PlotWavFile(maskOnTone);

            SharedFuncs.LoadFramesAsync(maskOffTone, maskOnTone, 20);
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

        private void ShowFramesDialogs()
        {
            var frameForm1 = new FrameForm(maskOffTone, false)
            {
                Text = $"{maskOffTone.PlotTitle} frames",
                Left = 5,
                Top = 20
            };

            var frameForm2 = new FrameForm(maskOnTone, false)
            {
                Text = $"{maskOnTone.PlotTitle} frames",
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
            var frameForm1 = new FrameForm(maskOffTone, true)
            {
                Text = $"{maskOffTone.PlotTitle} frames autocorrelation",
                Left = 5,
                Top = 500
            };

            var frameForm2 = new FrameForm(maskOnTone, true)
            {
                Text = $"{maskOnTone.PlotTitle} frames autocorrelation",
                Left = 935,
                Top = 500
            };

            frameForm1.Show();
            frameForm2.Show();
            new F0Form(maskOffTone, maskOnTone).Show();
        }

        private async void ButtonCenterClipping_Click(object sender, EventArgs e)
        {
            var clipTask1 = SharedFuncs.CenterClippingAsync(maskOnTone);
            var clipTask2 = SharedFuncs.CenterClippingAsync(maskOffTone);
            await Task.WhenAll(clipTask1, clipTask2);
            ShowFramesDialogs();

            var autoTask1 = SharedFuncs.AutocorrelationAsync(maskOnTone);
            var autoTask2 = SharedFuncs.AutocorrelationAsync(maskOffTone);
            await Task.WhenAll(autoTask1, autoTask2);
            ShowAutocorrelationsDialogs();
        }

        private async void ButtonDFT_Click(object sender, EventArgs e)
        {
            await SharedFuncs.DFT_AllFrames(maskOffTone);
            var specform1 = new SpectrogramForm(maskOffTone);
            specform1.Show();

            await SharedFuncs.DFT_AllFrames(maskOnTone);
            var specform2 = new SpectrogramForm(maskOnTone);
            specform2.Show();
        }
    }
}
