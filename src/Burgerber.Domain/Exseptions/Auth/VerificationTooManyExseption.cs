using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.Domain.Exseptions.Auth
{
    public class VerificationTooManyExseption:TooManyRequest
    {
        public VerificationTooManyExseption()
        {
            TitleMessage = "You tried more than limit";
        }
    }
}
