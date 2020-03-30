using System;
using System.Collections.Concurrent;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;

namespace MongoHell.Connection
{
	public sealed class ConnectionManager : IConnectionManager
	{
		public bool TransactionsAvailable => _transactionsAvailable;

		public ConnectionManager(string connectionString)
		{
			var url = new MongoUrl(connectionString);
			var mongoClientSettings = MongoClientSettings.FromUrl(url);

			_client = new MongoClient(mongoClientSettings);

			using (_client.StartSession())
			{
				_transactionsAvailable = _client.Cluster.Description.Type != ClusterType.Standalone;
			}
		}

		public ConnectionManager(MongoClientSettings settings)
		{
			_client = new MongoClient(settings);

			using (_client.StartSession())
			{
				_transactionsAvailable = _client.Cluster.Description.Type != ClusterType.Standalone;
			}
		}

		public IMongoDatabase GetDatabase(string dbName) =>
			_databases.GetOrAdd(dbName, name => _client.GetDatabase(name));

		public IClientSessionHandle GetNewSession() => _client.StartSession();

		private readonly MongoClient _client;
		private readonly bool _transactionsAvailable;

		private readonly ConcurrentDictionary<string, IMongoDatabase> _databases =
			new ConcurrentDictionary<string, IMongoDatabase>(StringComparer.InvariantCultureIgnoreCase);
	}
}