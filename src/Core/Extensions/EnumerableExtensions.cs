﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Extensions
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Performs the specified delegate on each element of a collection. 
		/// For each asynchronously.
		/// </summary>
		/// <param name="source">Source collection</param>
		/// <param name="partitionCount">Degree of parallelism</param>
		/// <param name="function">Delegate</param>
		/// <typeparam name="T">Input type</typeparam>
		/// <returns>Task</returns>
		public static Task ForEachAsync<T>(this IEnumerable<T> source, int partitionCount, Func<T, Task> function)
		{
			var partitions = Partitioner.Create(source).GetPartitions(partitionCount);
			var tasks = partitions.Select(partition =>
				Task.Run(async () =>
				{
					using (partition)
					{
						while (partition.MoveNext())
						{
							var sourceItem = partition.Current;
							await function(sourceItem).ConfigureAwait(false);
						}
					}
				}));

			return Task.WhenAll(tasks);
		}
	}
}