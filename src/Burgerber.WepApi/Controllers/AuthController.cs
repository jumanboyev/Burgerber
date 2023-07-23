using Burgerber.Service.Dtos.Auth;
using Burgerber.Service.Interfeces.Auth;
using Burgerber.Service.Validators.Dtos;
using Burgerber.Service.Validators.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burgerber.WepApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
    {
        var validator= new RegisterValidator();
        var result=validator.Validate(registerDto);
        if (result.IsValid)
        {
           var serciceResult = await _authService.RegisterAsync(registerDto);
            return Ok(new {serciceResult.Result,serciceResult.CachedMinutes});
        }
        else return BadRequest(result.Errors);
    }

    [HttpPost("register/send-code")]
    public async Task<IActionResult> SendCodeRegisterAsync(string phone)
    {
        var result=PhoneNumberValidator.isValid(phone);
        if (result == false) return BadRequest("Telefon raqam Yaroqsiz");

        var serviceResult=await _authService.SendCodeRegisterAsync(phone);
        return Ok(new {serviceResult.Result,serviceResult.CashedVerificationMinutes});
    }


}
