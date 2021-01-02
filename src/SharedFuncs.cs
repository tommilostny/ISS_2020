using NAudio.Wave;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ProjectISS
{
    public static class SharedFuncs
    {
        public const int Fs = 16000;           //16000 = 1s (Fs = 16kHz)

        public const int AutocorrThreshold = 10;

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

        private static async Task MeanNormalize(DataPoint[] points)
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

        public static async void MeanNormalize(SamplesData x1, SamplesData x2)
        {
            var task1 = MeanNormalize(x1.DataPoints);
            var task2 = MeanNormalize(x2.DataPoints);

            x1.IsNormalized = x2.IsNormalized = true;
            await Task.WhenAll(task1, task2);
        }


        private static async Task LoadFrames(SamplesData x, double frameLengthMs)
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

        public static async void LoadFrames(SamplesData x1, SamplesData x2, int frameLengthMs)
        {
            var task1 = LoadFrames(x1, frameLengthMs);
            var task2 = LoadFrames(x2, frameLengthMs);
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

        private static async Task CenterClipping(Frame frame)
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

        public static async Task CenterClipping(SamplesData x)
        {
            var tasks = new List<Task>();
            foreach (var frame in x.Frames)
            {
                tasks.Add(CenterClipping(frame));
            }
            await Task.WhenAll(tasks);
        }

        private static async Task<double> Autocorrelation(Frame frame)
        {
            return await Task.Run(() =>
            {
                int N = frame.DataPoints.Length;
                int lagIndex = AutocorrThreshold;
                for (int k = 0; k < N; k++)
                {
                    var tmp = new List<double>();
                    for (int n = 0; n < N - k; n++)
                    {
                        tmp.Add(frame.DataPoints[n].Y * frame.DataPoints[n + k].Y);
                    }
                    double sum = tmp.Sum() / N;
                    frame.AutocorrelationCoeficients[k] = new(k, sum);

                    if (k >= AutocorrThreshold && sum > frame.LagPoint.Y)
                    {
                        frame.LagPoint = frame.AutocorrelationCoeficients[k];
                        lagIndex = k;
                    }
                }
                return (double)Fs / lagIndex;
            });
        }

        public static async Task Autocorrelation(SamplesData x)
        {
            var tasks = new List<Task<double>>();
            x.F0Points = new DataPoint[x.Frames.Count];

            //run autocorrelation for every frame
            foreach (var frame in x.Frames)
            {
                tasks.Add(Autocorrelation(frame));
            }

            //get lag indexes from each autocorrelation, transform into f0
            for (int i = 0; i < tasks.Count; i++)
            {
                x.F0Points[i] = new(i, await tasks[i]);
            }
        }

        private static async Task<Complex[]> DFT(Frame frame, int N)
        {
            return await Task.Run(() =>
            {
                var result = new Complex[N];

                for (int k = 0; k < N; k++)
                {
                    var reals = new List<double>();
                    var imags = new List<double>();
                    for (int n = 0; n < N; n++)
                    {
                        if (n < frame.DataPoints.Length)
                        {
                            double arg = 2 * Math.PI * k * n / N;
                            reals.Add(frame.DataPoints[n].Y * Math.Cos(arg));
                            imags.Add(frame.DataPoints[n].Y * Math.Sin(arg));
                        }
                        else break; //zero padding (adding zeros to sums does nothing)
                    }
                    result[k] = new Complex(reals.Sum(), -imags.Sum());
                }
                return result;
            });
        }

        public static async Task DFT(SamplesData x, int N = 1024)
        {
            var tasks = new List<Task<Complex[]>>();
            foreach (var frame in x.Frames)
            {
                tasks.Add(DFT(frame, N));
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                x.Frames[i].DFTCoeficients = await tasks[i];
            }
        }
        
        public static DataPoint[] FrequencyChar(SamplesData x, SamplesData y, int N = 1024)
        {
            var result = new DataPoint[N / 2];
            for (int k = 0; k < N / 2; k++)
            {
                var a = new List<double>();
                var b = new List<double>();

                for (int i = 0; i < x.Frames.Count; i++)
                {
                    a.Add(Complex.Abs(x.Frames[i].DFTCoeficients[k]));
                    b.Add(Complex.Abs(y.Frames[i].DFTCoeficients[k]));
                }
                result[k] = new(k, b.Average() / (1 + a.Average()));
            }
            return result;
        }
    }
}
