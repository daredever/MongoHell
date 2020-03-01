using System.Threading.Tasks;
using Core;

namespace MongoDBRepository.Simple
{
	public class SimpleRepositoryWithProfiling<T> : SimpleRepository<T> where T : IMongoModel
	{
		public SimpleRepositoryWithProfiling(string connectionString, string databaseName) 
			: base(connectionString, databaseName)
		{
		}

		public override Task AddOrUpdateItemAsync(T item, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(SimpleRepository<T>.AddOrUpdateItemAsync));
			return base.AddOrUpdateItemAsync(item, collectionName);
		}

		public override Task<T> GetItemAsync(string id, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(SimpleRepository<T>.GetItemAsync));
			return base.GetItemAsync(id, collectionName);
		}

		public override Task DeleteItemAsync(string id, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(SimpleRepository<T>.DeleteItemAsync));
			return base.DeleteItemAsync(id, collectionName);
		}
	}
}