namespace Burgerber.Domain.Entities.Orders;

public class OrderDetail:Auditable
{
    public long ProductId { get; set; }
    public long OrderId { get; set; }
    public int Quantity { get; set; }

    //product_price * quantity
    public double TotalPrice { get; set; }
    public double DiscountPrice { get; set; }

    // total_price-discount_price
    public double ResultPrice { get; set; }

}
