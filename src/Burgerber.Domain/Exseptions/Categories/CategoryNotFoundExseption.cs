using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.Domain.Exseptions.Categories
{
    public class CategoryNotFoundExseption:NotFoundExseption
    {
        public CategoryNotFoundExseption()
        {
            this.TitleMessage = "Category Not Found";
        }
    }
}
