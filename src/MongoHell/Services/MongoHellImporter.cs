using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Core.Services;
using Core.Repositories;
using MongoHell.Models;

namespace MongoHell.Services
{
	public class MongoHellImporter : IMongoHellImporter
	{
		private const int Partitions = 20;
		private readonly IBaseRepository _mongoRepo;

		public MongoHellImporter(IBaseRepository mongoRepo) => _mongoRepo = mongoRepo;

		public async Task ImportAsync()
		{
			var hellItems = GetHellDataFromSource();
			await hellItems.ForEachAsync(Partitions, Add);

			var anotherHellItems = GetAnotherHellDataFromSource();
			await anotherHellItems.ForEachAsync(Partitions, Add);
		}

		private Task Add<T>(T item) where T : IBaseModel => _mongoRepo.AddOrUpdateItemAsync(item, item.GetType().Name);

		private IEnumerable<HellModel> GetHellDataFromSource()
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

		private IEnumerable<AnotherHellModel> GetAnotherHellDataFromSource()
		{
			for (var i = 0; i < 1000; i++)
			{
				yield return new AnotherHellModel
				{
					ExternalId = Guid.NewGuid().ToString(),
					Title = $"{nameof(AnotherHellModel)}_{i}"
				};
			}
		}
	}
}