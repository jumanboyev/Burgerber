using Burgerber.Service.Dtos.Auth;
using Burgerber.Service.Interfeces.Auth;
using Burgerber.Service.Validators.Dtos;
using Burgerber.Service.Validators.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("register"),AllowAnonymous]
    
    public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
    {
        var validator = new RegisterValidator();
        var result = validator.Validate(registerDto);
        if (result.IsValid)
        {
            var serciceResult = await _authService.RegisterAsync(registerDto);
            return Ok(new { serciceResult.Result, serciceResult.CachedMinutes });
        }
        else return BadRequest(result.Errors);
    }

    [HttpPost("register/send-code")]
    [AllowAnonymous]
    public async Task<IActionResult> SendCodeRegisterAsync(string phone)
    {
        var result = PhoneNumberValidator.isValid(phone);
        if (result == false) return BadRequest("Telefon raqam Yaroqsiz");

        var serviceResult = await _authService.SendCodeRegisterAsync(phone);
        return Ok(new { serviceResult.Result, serviceResult.CashedVerificationMinutes });
    }

    [HttpPost("register/verify")]
    [AllowAnonymous]

    public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
    {
        var serviceResult = await _authService.VerifyRegisterAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);
        return Ok(new { serviceResult.Result, serviceResult.Token });
    }

    [HttpPost("login")]
    [AllowAnonymous]

    public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var validator = new LoginValidator();
        var varResult = validator.Validate(loginDto);

        if (varResult.IsValid == false) return BadRequest(varResult.Errors);

        var serviceResult = await _authService.LoginAsync(loginDto);
        return Ok(new { serviceResult.Result, serviceResult.Token });
    }
}
