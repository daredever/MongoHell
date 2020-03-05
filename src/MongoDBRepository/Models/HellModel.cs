using System;
using Core.Models;

namespace MongoDBRepository.Models
{
	public sealed class HellModel : IBaseModel
	{
		public string ExternalId { get; set; }

		public string Name { get; set; }

		public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
	}
}