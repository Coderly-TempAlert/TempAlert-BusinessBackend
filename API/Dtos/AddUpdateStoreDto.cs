using Core.Entities;

namespace API.Dtos;

public class AddUpdateStoreDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}
