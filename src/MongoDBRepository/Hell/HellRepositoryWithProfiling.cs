using System.Threading.Tasks;
using Core.Profiling;
using MongoDBRepository.Models;

namespace MongoDBRepository.Hell
{
	public class HellRepositoryWithProfiling : HellRepository
	{
		public HellRepositoryWithProfiling(string connectionString, string databaseName) : base(connectionString, databaseName)
		{
			
		}

		public override Task AddOrUpdateItemAsync(HellModel item, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(HellRepository.AddOrUpdateItemAsync));
			return base.AddOrUpdateItemAsync(item, collectionName);
		}

		public override Task<HellModel> GetItemAsync(string id, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(HellRepository.GetItemAsync));
			return base.GetItemAsync(id, collectionName);
		}

		public override Task DeleteItemAsync(string id, string collectionName)
		{
			using var profiler = Profiler.GetProfiler(nameof(HellRepository.DeleteItemAsync));
			return base.DeleteItemAsync(id, collectionName);
		}
	}
}