using System;
using Core.Models;

namespace MongoHell.Models
{
	public sealed class HellModel : IBaseModel
	{
		public string ExternalId { get; set; }
		public string Name { get; set; }
		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}