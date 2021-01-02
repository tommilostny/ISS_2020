using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Numerics;
using System.Windows.Forms;

namespace ProjectISS
{
    public partial class SpectrogramForm : Form
    {
        public SpectrogramForm(SamplesData data)
        {
            InitializeComponent();

            var model = new PlotModel { Title = $"DFT spektrum ({data.PlotTitle})" };

            model.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Rainbow(100)
            });

            int topX = data.Frames.Count;
            int topY = SharedFuncs.Fs / 2;

            var heatmapData = new double[topX, topY];
            int N = data.DFTCoeficients[0].Length;

            for (int x = 0; x < topX; x++)
            {
                for (int k = 0; k < N; k++)
                {
                    int freq = k * SharedFuncs.Fs / N / 2;
                    heatmapData[x, freq] = 10 * Math.Log10(Math.Pow(Complex.Abs(data.DFTCoeficients[x][k]), 2));
                }
            }

            var x_axis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                Title = "rámce"
            };

            var y_axis = new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                Title = "frekvence [Hz]"
            };

            var heatMapSeries = new HeatMapSeries
            {
                X0 = 0,
                X1 = topX,
                Y0 = 0,
                Y1 = topY,
                Interpolate = true,
                RenderMethod = HeatMapRenderMethod.Bitmap,
                Data = heatmapData
            };

            model.Axes.Add(x_axis);
            model.Axes.Add(y_axis);
            model.Series.Add(heatMapSeries);
            plotView1.Model = model;
        }
    }
}
