using System.Text.Json.Serialization;

namespace Core.Entities;

public class StoreProducts
{

    public Guid StoreId { get; set; }
    public Store Store { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

}
