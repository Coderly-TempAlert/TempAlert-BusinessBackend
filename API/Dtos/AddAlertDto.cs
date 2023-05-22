using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class AddAlertDto
{
    [Required]
    public Guid StoreId { get; set; }

    [Required]
    public Guid ProductId { get; set; }
}
