using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ProjectISS
{
    public partial class FrameForm : Form
    {
        private SamplesData Data { get; }

        private int lastIndex;

        public FrameForm(SamplesData data)
        {
            InitializeComponent();
            Data = data;
            PlotFrame(0);

            for (int i = 0; i < data.Frames.Count - 1; i += 5)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void PlotFrame(int index)
        {
            button1.Enabled = index != 0;
            button2.Enabled = index != Data.Frames.Count - 1;
            lastIndex = index;
            label1.Text = $"Frame: {lastIndex + 1}/{Data.Frames.Count}";

            var x_axis = new LinearAxis
            {
                Maximum = Data.Frames[index].DataPoints.Last().X,
                Minimum = Data.Frames[index].DataPoints.First().X,
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false
            };
            var y_axis = new LinearAxis
            {
                Maximum = 1,
                Minimum = -1,
                Position = AxisPosition.Left,
                IsZoomEnabled = false
            };

            var pm = new PlotModel
            {
                Title = Data.PlotTitle,
            };
            pm.Axes.Add(x_axis);
            pm.Axes.Add(y_axis);

            var lineSeries = new LineSeries
            {
                Color = OxyColors.DodgerBlue
            };
            lineSeries.Points.AddRange(Data.Frames[index].DataPoints);
            pm.Series.Add(lineSeries);

            plotView1.Model = pm;
        }

        private void ButtonPrev_Click(object sender, EventArgs e)
        {
            PlotFrame(lastIndex - 1);
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            PlotFrame(lastIndex + 1);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlotFrame(Convert.ToInt32(comboBox1.Items[comboBox1.SelectedIndex]));
        }
    }
}
