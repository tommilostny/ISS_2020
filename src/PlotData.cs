using OxyPlot.Axes;

namespace src
{
    public record PlotData
    {
        public string PlotTitle { get; init; }
        public string FileName { get; init; }
        public string FullFilePath => $@"..\..\..\..\audio\{FileName}";
        public Axis XAxis { get; set; }
    }
}
