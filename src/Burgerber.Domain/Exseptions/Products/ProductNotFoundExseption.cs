namespace Burgerber.Domain.Exseptions.Products;

public class ProductNotFoundExseption : NotFoundExseption
{
    public ProductNotFoundExseption()
    {
        this.TitleMessage = "Product Not Found ";
    }
}
