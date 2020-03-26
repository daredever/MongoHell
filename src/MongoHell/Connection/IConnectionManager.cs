﻿using MongoDB.Driver;

 namespace MongoHell.Connection
{
	/// <summary>
	/// Connection manager for MongoDB.
	/// </summary>
	public interface IConnectionManager
	{
		
		/// <summary>
		/// Returns true if transactions are available.  
		/// </summary>
		bool TransactionsAvailable { get; }

		/// <summary>
		/// Returns database by name.
		/// </summary>
		/// <param name="dbName">Database name.</param>
		/// <exception cref="InvalidOperationException">Thrown if manager was not initialized.</exception>
		/// <returns>Database</returns>
		IMongoDatabase GetDatabase(string dbName);

		/// <summary>
		/// Opens a new client session. 
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown if manager was not initialized.</exception>
		/// <returns>New client session.</returns>
		IClientSessionHandle GetNewSession();
	}
}