using System.Threading.Tasks;
using Core.Profiling;

namespace MongoHell.Repository
{
	public class MongoHellRepositoryWithProfiling : MongoHellBaseRepository
	{
		public MongoHellRepositoryWithProfiling(string connectionString, string databaseName) : base(connectionString, databaseName)
		{
		}

		public override async Task AddOrUpdateItemAsync<T>(T item, string collectionName)
		{
			using var profiler = Profiler.GetProfiler($"{nameof(AddOrUpdateItemAsync)}({typeof(T).Name}, \"{collectionName}\")");
			await base.AddOrUpdateItemAsync(item, collectionName);
		}

		public override async Task<T> GetItemAsync<T>(string id, string collectionName)
		{
			using var profiler = Profiler.GetProfiler($"{nameof(GetItemAsync)}(\"{id}\", \"{collectionName}\")");
			return await base.GetItemAsync<T>(id, collectionName);
		}

		public override async Task DeleteItemAsync<T>(string id, string collectionName)
		{
			using var profiler = Profiler.GetProfiler($"{nameof(DeleteItemAsync)}(\"{id}\", \"{collectionName}\")");
			await base.DeleteItemAsync<T>(id, collectionName);
		}
	}
}