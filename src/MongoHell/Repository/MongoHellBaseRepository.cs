using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace MongoHell.Repository
{
	public class MongoHellBaseRepository : IBaseRepository
	{
		private readonly IMongoDatabase _mongoDatabase;

		public MongoHellBaseRepository(string connectionString, string databaseName)
		{
			var pack = new ConventionPack {new IgnoreExtraElementsConvention(true)};
			ConventionRegistry.Register("Hell conventions", pack, t => true);

			var client = new MongoClient(connectionString);
			_mongoDatabase = client.GetDatabase(databaseName);
		}

		public virtual Task AddOrUpdateItemAsync<T>(T item, string collectionName) where T : IBaseModel
		{
			var keyCollection = _mongoDatabase.GetCollection<T>(collectionName);
			var options = new ReplaceOptions {IsUpsert = true};
			return keyCollection.ReplaceOneAsync(x => x.ExternalId == item.ExternalId, item, options);
		}

		public virtual async Task<T> GetItemAsync<T>(string id, string collectionName) where T : IBaseModel
		{
			var keyCollection = _mongoDatabase.GetCollection<T>(collectionName);
			using var cursor = await keyCollection.FindAsync(x => x.ExternalId == id);
			return await cursor.SingleOrDefaultAsync();
		}

		public virtual Task DeleteItemAsync<T>(string id, string collectionName) where T : IBaseModel
		{
			var keyCollection = _mongoDatabase.GetCollection<T>(collectionName);
			return keyCollection.DeleteOneAsync(x => x.ExternalId == id);
		}
	}
}