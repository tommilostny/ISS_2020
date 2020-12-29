﻿using OxyPlot;
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

        public List<Frame> Frames { get; } = new List<Frame>();
    }

    public record Frame
    {
        public DataPoint[] DataPoints { get; }

        public Frame(DataPoint[] dataPoints, int startIndex, int length)
        {
            DataPoints = new DataPoint[length];
            for (int i = startIndex; i < startIndex + length; i++)
            {
                DataPoints[i - startIndex] = new(dataPoints[i].X, dataPoints[i].Y);
            }
        }
    }
}
