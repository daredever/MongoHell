using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
	public interface IBaseRepository
	{
		Task AddOrUpdateItemAsync<T>(T item, string collectionName) where T: IBaseModel;
		Task<T> GetItemAsync<T>(string id, string collectionName) where T: IBaseModel;
		Task DeleteItemAsync<T>(string id, string collectionName) where T: IBaseModel;
	}
}