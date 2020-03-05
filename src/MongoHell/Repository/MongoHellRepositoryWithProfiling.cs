using System.Threading.Tasks;
using Core.Profiling;

namespace MongoHell.Repository
{
	public class MongoHellRepositoryWithProfiling : MongoHellBaseRepository
	{
		public MongoHellRepositoryWithProfiling(string connectionString, string databaseName)
			: base(connectionString, databaseName)
		{
		}

		public override Task AddOrUpdateItemAsync<T>(T item, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(base.AddOrUpdateItemAsync));
			return base.AddOrUpdateItemAsync(item, collectionName);
		}

		public override Task<T> GetItemAsync<T>(string id, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(base.GetItemAsync));
			return base.GetItemAsync<T>(id, collectionName);
		}

		public override Task DeleteItemAsync<T>(string id, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(base.DeleteItemAsync));
			return base.DeleteItemAsync<T>(id, collectionName);
		}
	}
}