using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burgerber.Domain.Exseptions.Auth;

public class VerificationCodeExpiredExseption:ExpiredExseption
{
    public VerificationCodeExpiredExseption()
    {
        TitleMessage = "Verification code is Expired";
    }
}
