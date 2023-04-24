using System.Text.Json.Serialization;

namespace Core.Entities;

public class Store : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    public ICollection<StoreProducts> StoreProducts { get; set; }
}