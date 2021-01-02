using OxyPlot;
using System.Numerics;

namespace ProjectISS
{
    public record Frame
    {
        public DataPoint[] DataPoints { get; }

        public DataPoint[] AutocorrelationCoeficients { get; }

        public DataPoint LagPoint { get; set; }

        public Complex[] DFTCoeficients { get; set; }

        public Frame(DataPoint[] dataPoints, int startIndex, int length)
        {
            AutocorrelationCoeficients = new DataPoint[length];
            DataPoints = new DataPoint[length];
            for (int i = startIndex; i < startIndex + length; i++)
            {
                DataPoints[i - startIndex] = new(dataPoints[i].X, dataPoints[i].Y);
            }
        }
    }
}
