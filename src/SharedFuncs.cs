using NAudio.Wave;
using NumSharp.Extensions;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectISS
{
    public static class SharedFuncs
    {
        public const int Fs = 16000;           //16000 = 1s (Fs = 16kHz)

        public static DataPoint[] LoadAudioSamples(string filePath, int samplesCount = Fs)
        {
            var datapoints = new DataPoint[samplesCount];
            var samples = new short[samplesCount];

            //Read WAV file into array of 16-bit integers
            using (var waveFileReader = new WaveFileReader(filePath))
            {
                var buffer = new byte[samplesCount * 2];
                waveFileReader.Read(buffer, 0, buffer.Length);
                Buffer.BlockCopy(buffer, 0, samples, 0, samplesCount * 2);
            }

            //Convert read array to datapoints for OxyPlot
            for (int i = 0; i < samplesCount; i++)
            {
                //datapoint.X: time
                //datapoint.Y: sample value
                datapoints[i] = new DataPoint((double)i / Fs, samples[i]);
            }
            return datapoints;
        }

        private static async Task MeanNormalizeRoutineAsync(DataPoint[] points)
        {
            await Task.Run(() =>
            {
                //ustřednění (x -= np.mean(x)), ekvivalent Average
                double mean = points.Average(point => point.Y);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new(points[i].X, points[i].Y - mean);
                }

                //normalizace (x /= np.abs(x).max())
                double max = points.Max(point => Math.Abs(point.Y));
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = new(points[i].X, points[i].Y / max);
                }
            });
        }

        public static async void MeanNormalizeAsync(SamplesData x1, SamplesData x2)
        {
            var task1 = MeanNormalizeRoutineAsync(x1.DataPoints);
            var task2 = MeanNormalizeRoutineAsync(x2.DataPoints);

            x1.IsNormalized = x2.IsNormalized = true;
            await Task.WhenAll(task1, task2);
        }


        private static async Task FramesRoutineAsync(SamplesData x, double frameLengthMs)
        {
            await Task.Run(() =>
            {
                //vzorec pro výpočet velikosti rámce ve vzorcích
                int frameLength = (int)(frameLengthMs / 1000 * Fs);

                for (int i = 0; i < x.DataPoints.Length - frameLength / 2; i += frameLength / 2)
                {
                    x.Frames.Add(new Frame(x.DataPoints, i, frameLength));
                }
            });
        }

        public static async void LoadFramesAsync(SamplesData x1, SamplesData x2, int frameLengthMs)
        {
            var task1 = FramesRoutineAsync(x1, frameLengthMs);
            var task2 = FramesRoutineAsync(x2, frameLengthMs);
            await Task.WhenAll(task1, task2);
        }

        public static PlotModel PlotWavFile(SamplesData plotData)
        {
            //OxyPlot model setup
            var pm = new PlotModel
            {
                Title = plotData.PlotTitle,
                Subtitle = plotData.FileName,
                Background = OxyColors.White,
            };

            //XY axes setup
            plotData.XAxis = new LinearAxis
            {
                Maximum = plotData.Seconds,
                Minimum = 0,
                Position = AxisPosition.Bottom
            };
            var y_axis = new LinearAxis
            {
                Maximum = plotData.IsNormalized ? 1 : 1100,
                Minimum = plotData.IsNormalized ? -1 : -1100,
                Position = AxisPosition.Left,
                IsZoomEnabled = false
            };
            pm.Axes.Add(plotData.XAxis);
            pm.Axes.Add(y_axis);

            //Setup stem
            var lineSeries = new LineSeries
            {
                Color = OxyColors.DodgerBlue
            };
            lineSeries.Points.AddRange(plotData.DataPoints);
            pm.Series.Add(lineSeries);

            return pm;
        }

        private static async Task CenterClippingRoutineAsync(Frame frame)
        {
            await Task.Run(() =>
            {
                /* vzorky, které jsou nad 70 % maxima absolutní hodnoty převedeme na 1,
                 * pod 70 % záporného maxima absolutní hodnoty převedeme na -1 a ostatní na 0
                 */
                double threshold = frame.DataPoints.Max(point => Math.Abs(point.Y)) * 0.7;

                for (int i = 0; i < frame.DataPoints.Length; i++)
                {
                    if (frame.DataPoints[i].Y > threshold)
                    {
                        frame.DataPoints[i] = new(frame.DataPoints[i].X, 1);
                    }
                    else if (frame.DataPoints[i].Y < -threshold)
                    {
                        frame.DataPoints[i] = new(frame.DataPoints[i].X, -1);
                    }
                    else
                    {
                        frame.DataPoints[i] = new(frame.DataPoints[i].X, 0);
                    }
                }
            });
        }

        public static async Task CenterClippingAsync(SamplesData x)
        {
            var tasks = new List<Task>();
            foreach (var frame in x.Frames)
            {
                tasks.Add(CenterClippingRoutineAsync(frame));
            }
            await Task.WhenAll(tasks);
        }

        private static async Task AutocorrelationRoutineAsync(Frame frame)
        {
            await Task.Run(() =>
            {
                int N = frame.DataPoints.Length;
                for (int k = 0; k < N; k++)
                {
                    var tmp = new List<double>();
                    for (int n = 0; n < N - k - 1; n++)
                    {
                        tmp.Add(frame.DataPoints[n].Y * frame.DataPoints[n + k].Y);
                    }
                    double sum = tmp.Sum();
                    frame.AutocorrelationCoeficients[k] = new(k, sum);

                    if (k >= 32 && (k == 32 || sum > frame.LagPoint.Y))
                    {
                        frame.LagPoint = frame.AutocorrelationCoeficients[k];
                    }
                }
            });
        }

        public static async Task AutocorrelationAsync(SamplesData x)
        {
            var tasks = new List<Task>();
            foreach (var frame in x.Frames)
            {
                tasks.Add(AutocorrelationRoutineAsync(frame));
            }
            await Task.WhenAll(tasks);
        }
    }
}
