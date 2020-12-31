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

        private readonly bool isAutocorrelation;

        public FrameForm(SamplesData data, bool isAutocorrelation)
        {
            InitializeComponent();
            Data = data;
            this.isAutocorrelation = isAutocorrelation;

            for (int i = 0; i < data.Frames.Count - 1; i += 5)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;

            if (isAutocorrelation)
                PlotAutocorrelation(0);
            else
                PlotFrame(0);
        }

        private void SetNavButtons(int index)
        {
            button1.Enabled = index != 0;
            button2.Enabled = index != Data.Frames.Count - 1;
            lastIndex = index;
            label1.Text = $"Frame: {lastIndex + 1}/{Data.Frames.Count}";
        }

        private void PlotFrame(int index)
        {
            SetNavButtons(index);

            var x_axis = new LinearAxis
            {
                Maximum = Data.Frames[index].DataPoints.Last().X,
                Minimum = Data.Frames[index].DataPoints.First().X,
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                Title = "time"
            };
            var y_axis = new LinearAxis
            {
                Maximum = 1.05,
                Minimum = -1.05,
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                Title = "y"
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
            if (isAutocorrelation)
                PlotAutocorrelation(lastIndex - 1);
            else
                PlotFrame(lastIndex - 1);
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (isAutocorrelation)
                PlotAutocorrelation(lastIndex + 1);
            else
                PlotFrame(lastIndex + 1);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(comboBox1.Items[comboBox1.SelectedIndex]);

            if (isAutocorrelation)
                PlotAutocorrelation(index);
            else
                PlotFrame(index);
        }

        private void PlotAutocorrelation(int frameIndex)
        {
            SetNavButtons(frameIndex);

            var x_axis = new LinearAxis
            {
                Maximum = Data.Frames[frameIndex].AutocorrelationCoeficients.Length,
                Minimum = 0,
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                Title = "vzorky"
            };
            var y_axis = new LinearAxis
            {
                Maximum = Data.Frames.Max(f => f.AutocorrelationCoeficients.Max(p => p.Y)) + 0.05,
                Minimum = Data.Frames.Min(f => f.AutocorrelationCoeficients.Min(p => p.Y)) - 0.05,
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                Title = "y"
            };

            var pm = new PlotModel
            {
                Title = $"Autokorelace ({Data.PlotTitle})",
            };
            pm.Axes.Add(x_axis);
            pm.Axes.Add(y_axis);

            var lineSeries = new LineSeries
            {
                Color = OxyColors.DodgerBlue
            };
            lineSeries.Points.AddRange(Data.Frames[frameIndex].AutocorrelationCoeficients);

            var thresholdPoint = new StemSeries
            {
                Color = OxyColors.Black,
                StrokeThickness = 1,
                Title = "Práh"
            };
            thresholdPoint.Points.Add(new DataPoint(SharedFuncs.AutocorrThreshold, 1000));
            thresholdPoint.Points.Add(new DataPoint(SharedFuncs.AutocorrThreshold, -1000));

            var lagPoint = new StemSeries
            {
                MarkerStroke = OxyColors.Red,
                MarkerFill = OxyColors.Red,
                MarkerType = MarkerType.Circle,
                MarkerStrokeThickness = 6,
                Title = "Lag"
            };
            lagPoint.Points.Add(Data.Frames[frameIndex].LagPoint);

            pm.Series.Add(lineSeries);
            pm.Series.Add(thresholdPoint);
            pm.Series.Add(lagPoint);

            plotView1.Model = pm;
        }
    }
}
