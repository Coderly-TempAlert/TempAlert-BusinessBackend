using System.Text.Json.Serialization;

namespace Core.Entities;

public class Store : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public DateTime CreatedDate { get; set; }
    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    [JsonIgnore]
    public ICollection<StoreProducts> StoreProducts { get; set; }
    [JsonIgnore]
    public ICollection<Alert> Alerts{ get; set; }
}