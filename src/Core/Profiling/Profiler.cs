using System;
using System.Diagnostics;
using System.Globalization;

namespace Core.Profiling
{
	public sealed class Profiler : IDisposable
	{
		private Profiler(string stage)
		{
			_stage = stage;
			_stopWatch = Stopwatch.StartNew();
		}

		public void Dispose()
		{
			_stopWatch.Stop();
			var workTime = Math.Round(_stopWatch.ElapsedTicks * MillisecPerTick, 4);
			Console.WriteLine($"[{DateTime.Now}] {_stage}, {workTime.ToString(CultureInfo.InvariantCulture)}");
		}

		public static Profiler GetProfiler(string stage) => new Profiler(stage);

		private static readonly decimal MillisecPerTick = 1000m / Stopwatch.Frequency;

		private readonly Stopwatch _stopWatch;
		private readonly string _stage;
	}
}