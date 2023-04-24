using System.Text.Json.Serialization;

namespace Core.Entities;

public class StoreProducts
{
    [JsonIgnore]
    public Guid StoreId { get; set; }
    [JsonIgnore]
    public Store Store { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

}
