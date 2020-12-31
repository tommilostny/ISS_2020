using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Linq;
using System.Windows.Forms;

namespace ProjectISS
{
    public partial class F0Form : Form
    {
        public F0Form(SamplesData x1, SamplesData x2)
        {
            InitializeComponent();

            var x_axis = new LinearAxis
            {
                Maximum = x1.F0Points.Length + 1,
                Minimum = 0,
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                Title = "rámce"
            };

            double maxY1 = x1.F0Points.Max(point => point.Y);
            double maxY2 = x2.F0Points.Max(point => point.Y);
            double minY1 = x1.F0Points.Min(point => point.Y);
            double minY2 = x2.F0Points.Min(point => point.Y);

            var y_axis = new LinearAxis
            {
                Maximum = (maxY1 > maxY2 ? maxY1 : maxY2) + 5,
                Minimum = (minY1 < minY2 ? minY1 : minY2) - 5,
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                Title = "f0"
            };

            var pm = new PlotModel();
            pm.Axes.Add(x_axis);
            pm.Axes.Add(y_axis);

            var lineSeries1 = new LineSeries
            {
                Color = OxyColors.DodgerBlue,
                Title = x1.PlotTitle
            };
            lineSeries1.Points.AddRange(x1.F0Points);

            var lineSeries2 = new LineSeries
            {
                Color = OxyColors.OrangeRed,
                Title = x2.PlotTitle
            };
            lineSeries2.Points.AddRange(x2.F0Points);

            pm.Series.Add(lineSeries1);
            pm.Series.Add(lineSeries2);

            plotView1.Model = pm;
        }
    }
}
