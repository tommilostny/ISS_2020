using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Linq;
using System.Windows.Forms;

namespace ProjectISS.Forms
{
    public partial class FreqCharForm : Form
    {
        public FreqCharForm(DataPoint[] freqCharPoints, string title)
        {
            InitializeComponent();

            var x_axis = new LinearAxis
            {
                Maximum = freqCharPoints.Length - 1,
                Minimum = 0,
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
            };

            var y_axis = new LinearAxis
            {
                Maximum = freqCharPoints.Max(point => point.Y),
                Minimum = freqCharPoints.Min(point => point.Y),
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
            };

            var pm = new PlotModel { Title = title };
            pm.Axes.Add(x_axis);
            pm.Axes.Add(y_axis);

            var lineSeries = new LineSeries
            {
                Color = OxyColors.DodgerBlue,
            };
            lineSeries.Points.AddRange(freqCharPoints);

            pm.Series.Add(lineSeries);
            plotView1.Model = pm;
        }
    }
}
