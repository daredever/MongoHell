using System.Threading.Tasks;

namespace Core.Repositories
{
	public interface IBaseRepository<T>
	{
		Task AddOrUpdateItemAsync(T item, string collectionName);
		Task<T> GetItemAsync(string id, string collectionName);
		Task DeleteItemAsync(string id, string collectionName);
	}
}