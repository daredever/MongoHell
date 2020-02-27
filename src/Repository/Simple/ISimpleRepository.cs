﻿using System.Threading.Tasks;

namespace Repository.Simple
{
	public interface ISimpleRepository<T>
	{
		Task AddOrUpdateItemAsync(T item, string collectionName);
		Task<T> GetItemAsync(string id, string collectionName);
		Task DeleteItemAsync(string id, string collectionName);
	}
}