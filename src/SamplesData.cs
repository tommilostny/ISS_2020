using OxyPlot;
using OxyPlot.Axes;
using System.Collections.Generic;

namespace ProjectISS
{
    public record SamplesData
    {
        public string PlotTitle { get; init; }

        public string FileName { get; init; }
        public string FullFilePath => $@"..\..\..\..\audio\{FileName}";

        public Axis XAxis { get; set; }

        public double Seconds { get; init; }

        private DataPoint[] dps = null;
        public DataPoint[] DataPoints
        {
            get => dps switch
            {
                null => dps = SharedFuncs.LoadAudioSamples(FullFilePath, (int)(Seconds * SharedFuncs.Fs)),
                _ => dps
            };
        }

        public bool IsNormalized { get; set; } = false;

        public List<Frame> Frames { get; set; }

        public DataPoint[] F0Points { get; set; } = null;

        public DataPoint[] FreqCharPoints { get; set; } = null;
    }
}
