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
        private const int paletteColors = 128;

        public SpectrogramForm(SamplesData data)
        {
            InitializeComponent();
            RenderDFT(data);
            RenderScale();
        }

        private void RenderDFT(SamplesData data)
        {
            var model = new PlotModel { Title = $"DFT spektrum ({data.PlotTitle})" };
            model.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Rainbow(paletteColors)
            });

            int topX = data.Frames.Count;
            int topY = SharedFuncs.Fs / 2;

            var heatmapData = new double[topX, topY];
            int N = data.Frames[0].DFTCoeficients.Length;

            for (int x = 0; x < topX; x++)
            {
                for (int freq = 0; freq < topY; freq++)
                {
                    int k = (int)(freq / ((double)SharedFuncs.Fs / N));
                    heatmapData[x, freq] = 10 * Math.Log10(Math.Pow(Complex.Abs(data.Frames[x].DFTCoeficients[k]), 2));
                }
            }

            var x_axis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "čas [s]"
            };

            var y_axis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "frekvence [Hz]"
            };

            var heatMapSeries = new HeatMapSeries
            {
                X0 = 0,
                X1 = data.Seconds,
                Y0 = 0,
                Y1 = topY,
                Data = heatmapData
            };

            model.Axes.Add(x_axis);
            model.Axes.Add(y_axis);
            model.Series.Add(heatMapSeries);
            plotView1.Model = model;
        }

        private void RenderScale()
        {
            var scaleModel = new PlotModel();
            scaleModel.Axes.Add(new LinearColorAxis
            {
                Palette = OxyPalettes.Rainbow(paletteColors)
            });

            var scale = new double[2, paletteColors];
            for (int i = 0; i < paletteColors; i++)
            {
                scale[0, i] = i - paletteColors / 2;
                scale[1, i] = i - paletteColors / 2;
            }

            var scaleSeries = new HeatMapSeries
            {
                X0 = 0,
                X1 = 1,
                Y0 = -(paletteColors / 2),
                Y1 = paletteColors / 2,
                Data = scale
            };

            scaleModel.Series.Add(scaleSeries);
            plotView2.Model = scaleModel;
        }
    }
}
