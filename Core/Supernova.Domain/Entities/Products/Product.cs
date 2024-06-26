using Supernova.Domain.Entities.Common;

namespace Supernova.Domain;

public class Product : BaseEntity
{
	public string Name { get; set; }
	public int Stock { get; set; }
	public long Price { get; set; }
}
