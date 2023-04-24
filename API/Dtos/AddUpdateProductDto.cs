using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class AddUpdateProductDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [StringLength(maximumLength: 500, MinimumLength = 1, ErrorMessage = "The description lenght must be greater than 1 and less than 500")]
    public string Description { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The amount must be greater than 1")]
    public int Amount { get; set; }

    [Required]
    public Guid StoreId { get; set; }

    [Required]
    public decimal Temperature { get; set; }
}
