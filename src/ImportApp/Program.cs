using System;
using System.Threading.Tasks;
using MongoHell.Repository;
using MongoHell.Services;

namespace ImportApp
{
	internal static class Program
	{
		private const string ConnectionString = "mongodb://localhost:27017/";
		private const string Database = "MongoHell";

		private static async Task Main(string[] args)
		{
			var mongoRepo = new MongoHellRepositoryWithProfiling(ConnectionString, Database);
			var importer = new MongoHellImporter(mongoRepo);
			await importer.ImportAsync();
		}
	}
}