using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.Domain.Exseptions.Delivers;

public class DeliverNotFoundExseption:NotFoundExseption
{
    public DeliverNotFoundExseption()
    {
        this.TitleMessage = "Deliver not found ";
    }
}
