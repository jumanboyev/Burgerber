using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.Domain.Exseptions.Admin;

public class AdminNotFoundExseption:NotFoundExseption
{
    public AdminNotFoundExseption()
    {
        this.TitleMessage = "Admin not found";
    }
}
