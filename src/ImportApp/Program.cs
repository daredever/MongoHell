using System;
using System.Threading.Tasks;
using ImportApp.Services;
using MongoDBRepository.Hell;

namespace ImportApp
{
	internal static class Program
	{
		private const string ConnectionString = "mongodb://localhost:27017/";
		private const string Database = "MongoHell";

		private static async Task Main(string[] args)
		{
			var mongoRepo = new HellRepositoryWithProfiling(ConnectionString, Database);
			var importer = new HellImporter(mongoRepo);
			await importer.ImportAsync();
		}
	}
}