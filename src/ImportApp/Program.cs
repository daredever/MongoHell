using System;
using System.Threading.Tasks;

namespace ImportApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var importer = new Importer();
			await importer.ImportAsync();
		}
	}
}