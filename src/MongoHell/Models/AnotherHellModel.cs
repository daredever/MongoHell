using System;
using Core.Models;

namespace MongoHell.Models
{
	public sealed class AnotherHellModel : IBaseModel
	{
		public string ExternalId { get; set; }
		public string Title { get; set; }
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}