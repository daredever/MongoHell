using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImportApp.Extensions;
using MongoDBRepository.Hell;
using MongoDBRepository.Models;

namespace ImportApp.Services
{
	public class HellImporter
	{
		private const int Partitions = 20;
		private const string CollectionName = "HellModelCollection";
		
		private readonly HellRepository _mongoRepo;

		public HellImporter(HellRepository mongoRepo) => _mongoRepo = mongoRepo;

		public async Task ImportAsync()
		{
			var items = GetDataFromSource();
			await items.ForEachAsync(Partitions, Add);
		}
		
		private Task Add(HellModel item) => _mongoRepo.AddOrUpdateItemAsync(item, CollectionName);

		private IEnumerable<HellModel> GetDataFromSource()
		{
			for (var i = 0; i < 1000; i++)
			{
				yield return new HellModel
				{
					ExternalId = Guid.NewGuid().ToString(),
					Name = $"{nameof(HellModel)}_{i}"
				};
			}
		}
	}
}