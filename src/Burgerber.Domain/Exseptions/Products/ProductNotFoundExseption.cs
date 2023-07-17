using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.Domain.Exseptions.Products;

public class ProductNotFoundExseption:NotFoundExseption
{
    public ProductNotFoundExseption()
    {
        this.TitleMessage = "Product Not Found ";
    }
}
