namespace API.Dtos;

public class AddUpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public decimal Temperature { get; set; }
}
