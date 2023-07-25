using System.ComponentModel.DataAnnotations;

namespace Burgerber.Domain.Entities.Products;

public class Product : Auditable
{
    public long CategoryId { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public double UnitPrice { get; set; }

    public string Description { get; set; } = string.Empty;



}
