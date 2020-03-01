using System;
using System.Diagnostics;

namespace Core
{
	public sealed class Profiler : IDisposable
	{
		public Profiler(string stage)
		{
			_stage = stage;
			_stopWatch = Stopwatch.StartNew();
		}

		public void Dispose()
		{
			_stopWatch.Stop();
			
			var workTime = Math.Round(_stopWatch.ElapsedTicks * MillisecPerTick, 4);
			// write data with _stage and workTime in milliseconds
			Console.WriteLine($"{_stage}, {workTime}");
		}

		public static Profiler GetProfiler(string stage) => new Profiler(stage);

		private static readonly decimal MillisecPerTick = 1000m / Stopwatch.Frequency;

		private readonly Stopwatch _stopWatch;
		private readonly string _stage;
	}
}