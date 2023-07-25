namespace Burgerber.Domain.Entities.Discounts;

public class Discount : Auditable
{
    public string Name { get; set; } = string.Empty;

    public int Percentage { get; set; }

    public string Description { get; set; } = string.Empty;

}
