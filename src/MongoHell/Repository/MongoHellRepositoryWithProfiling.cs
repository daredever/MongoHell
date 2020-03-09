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
			var stage = $"{nameof(AddOrUpdateItemAsync)}({typeof(T).Name}, \"{collectionName}\")";
			using var profiler = Profiler.GetProfiler(stage);
			return base.AddOrUpdateItemAsync(item, collectionName);
		}

		public override Task<T> GetItemAsync<T>(string id, string collectionName)
		{
			var stage = $"{nameof(GetItemAsync)}(\"{id}\", \"{collectionName})\"";
			using var profiler = Profiler.GetProfiler(stage);
			return base.GetItemAsync<T>(id, collectionName);
		}

		public override Task DeleteItemAsync<T>(string id, string collectionName)
		{
			var stage = $"{nameof(DeleteItemAsync)}(\"{id}\", \"{collectionName})\"";
			using var profiler = Profiler.GetProfiler(stage);
			return base.DeleteItemAsync<T>(id, collectionName);
		}
	}
}