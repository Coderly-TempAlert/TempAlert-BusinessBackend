using System.Text.Json.Serialization;

namespace Core.Entities;

public class Alert
{
    public Guid StoreId { get; set; }
    [JsonIgnore]
    public Store Store { get; set; }
    public Guid ProductId { get; set; }
    [JsonIgnore]
    public Product Product { get; set; }
}

