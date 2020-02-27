﻿using System;

namespace Repository.Simple
{
	public sealed class Item : IMongoModel
	{
		public string ExternalId { get; set; }
		
		public DateTimeOffset Created { get; set; }
	}
}