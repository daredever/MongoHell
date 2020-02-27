using System.Threading.Tasks;
using MongoDB.Driver;

namespace Repository.Simple
{
	public class SimpleRepository<T> : ISimpleRepository<T> where T : IMongoModel
	{
		private readonly IMongoDatabase _mongoDatabase;

		public SimpleRepository(string connectionString, string databaseName)
		{
			var client = new MongoClient(connectionString);
			_mongoDatabase = client.GetDatabase(databaseName);
		}

		public Task AddOrUpdateItemAsync(T item, string collectionName)
		{
			var keyCollection = _mongoDatabase.GetCollection<T>(collectionName);
			var options = new ReplaceOptions {IsUpsert = true};

			return keyCollection.ReplaceOneAsync(x => x.ExternalId == item.ExternalId, item, options);
		}

		public async Task<T> GetItemAsync(string id, string collectionName)
		{
			var keyCollection = _mongoDatabase.GetCollection<T>(collectionName);
			using var cursor = await keyCollection.FindAsync(x => x.ExternalId == id);
			return await cursor.SingleOrDefaultAsync();
		}

		public Task DeleteItemAsync(string id, string collectionName)
		{
			var keyCollection = _mongoDatabase.GetCollection<T>(collectionName);
			return keyCollection.DeleteOneAsync(x => x.ExternalId == id);
		}
	}
}