namespace Burgerber.Domain.Entities.Products;

public class ProductDiscounts : Auditable
{
    public long ProductId { get; set; }

    public long DiscountId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Description { get; set; } = string.Empty;
}
