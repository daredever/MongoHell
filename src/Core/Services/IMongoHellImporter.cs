using System.Threading.Tasks;

namespace Core.Services
{
	public interface IMongoHellImporter
	{
		Task ImportAsync();
	}
}