namespace Burgerber.Domain.Entities.Orders;

public class Order : Auditable
{
    public long ClientId { get; set; }
    public long DeliverId { get; set; }

    // The summ of order details result prices
    // The monay which that user must pay for products
    public double ProductPrice { get; set; }

    public double DeliverPrice { get; set; }

    //product_price+deliver_price
    public double Totalprice { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Payment { set; get; } = string.Empty;

    public bool isContaracted { get; set; }

    public bool isPaid { set; get; }

    public string Description { set; get; } = string.Empty;


}


