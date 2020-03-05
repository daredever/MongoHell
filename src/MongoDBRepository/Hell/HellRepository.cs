﻿using System.Threading.Tasks;
using MongoDBRepository.Base;
using MongoDBRepository.Models;

namespace MongoDBRepository.Hell
{
	public class HellRepository : MongoDBBaseRepository<HellModel>
	{
		public HellRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
		{
		}
	}
}