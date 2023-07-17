using System.ComponentModel.DataAnnotations;

namespace Burgerber.Domain.Entities.Catagories;

public class Category:Auditable
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

}
