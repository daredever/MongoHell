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
		private readonly HellRepository _mongoRepo;
		private const int Partitions = 20;

		public HellImporter(HellRepository mongoRepo)
		{
			_mongoRepo = mongoRepo;
		}

		public async Task ImportAsync()
		{
			var collectionName = $"{nameof(HellModel)}";
			async Task Add(HellModel item) => await _mongoRepo.AddOrUpdateItemAsync(item, collectionName);

			var items = GetDataFromSource();
			await items.ForEachAsync(Partitions, Add);
		}

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