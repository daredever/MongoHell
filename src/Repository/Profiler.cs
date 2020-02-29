﻿using System;
using System.Diagnostics;

namespace Repository
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
			
			var timeInMillisec = Math.Round((decimal) (_stopWatch.ElapsedTicks * _nanosecPerTick) / _nanosecToMillisec, 4);

			// write data with _stage and timeInMillisec
		}

		public static Profiler GetProfiler(string stage) => new Profiler(stage);

		private static long _nanosecPerTick = 1000000000L / Stopwatch.Frequency;
		private static long _nanosecToMillisec = 1000000L;
		
		private readonly Stopwatch _stopWatch;
		private readonly string _stage;
	}
}