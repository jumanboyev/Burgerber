namespace Burgerber.Domain.Exseptions.Discounts;

public class DiscountNotFoundExseption:NotFoundExseption
{
    public DiscountNotFoundExseption()
    {
        this.TitleMessage = "Discount Not Found";
    }

}
