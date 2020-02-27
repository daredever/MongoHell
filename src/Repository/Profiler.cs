﻿using System;
using System.Diagnostics;

namespace Repository
{
	public sealed class Profiler : IDisposable
	{
		public Profiler(string stage, string key)
		{
			_stage = stage;
			_key = key;
			_stopwatch = Stopwatch.StartNew();
		}

		public void Dispose()
		{
			_stopwatch.Stop();
			var nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
			var nanosecToMillisec = 1000L * 1000L;
			var timeInMillisec = Math.Round((decimal) (_stopwatch.ElapsedTicks * nanosecPerTick) / nanosecToMillisec, 4);

			// write data with _stage, _key and timeInMillisec
		}

		public static Profiler GetProfiler(string stage, string key) => new Profiler(stage, key);

		private readonly Stopwatch _stopwatch;
		private readonly string _stage;
		private readonly string _key;
	}
}