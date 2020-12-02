using OxyPlot;
using OxyPlot.Axes;

namespace src
{
    public record PlotData
    {
        public string PlotTitle { get; init; }

        public string FileName { get; init; }
        public string FullFilePath => $@"..\..\..\..\audio\{FileName}";

        public Axis XAxis { get; set; }

        public double Seconds { get; init; }

        private DataPoint[] dps = null;
        public DataPoint[] DataPoints
        {
            get
            {
                if (dps == null)
                {
                    dps = Plotting.LoadAudioSamples(FullFilePath, (int)(Seconds * Plotting.Fs));
                    IsNormalized = false;
                }
                return dps;
            }
            set => dps = value;
        }

        public bool IsNormalized { get; set; } = false;
    }
}
