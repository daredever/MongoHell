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

		public override async Task AddOrUpdateItemAsync<T>(T item, string collectionName)
		{
			var stage = $"{nameof(AddOrUpdateItemAsync)}({typeof(T).Name}, \"{collectionName}\")";
			using var profiler = Profiler.GetProfiler(stage);
			await base.AddOrUpdateItemAsync(item, collectionName);
		}

		public override async Task<T> GetItemAsync<T>(string id, string collectionName)
		{
			var stage = $"{nameof(GetItemAsync)}(\"{id}\", \"{collectionName}\")";
			using var profiler = Profiler.GetProfiler(stage);
			return await base.GetItemAsync<T>(id, collectionName);
		}

		public override async Task DeleteItemAsync<T>(string id, string collectionName)
		{
			var stage = $"{nameof(DeleteItemAsync)}(\"{id}\", \"{collectionName}\")";
			using var profiler = Profiler.GetProfiler(stage);
			await base.DeleteItemAsync<T>(id, collectionName);
		}
	}
}