using Burgerber.Domain.Entities.Clients;
using Burgerber.Service.Commons.Helpers.TimeHelper;
using Burgerber.Service.Interfeces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Burgerber.Service.Services.Auth;

public class TokenService : ITokenService
{
    private IConfiguration _config;

    public TokenService(IConfiguration configuration)
    {
        this._config = configuration.GetSection("Jwt");
    }
    public async Task<string> GenerateToken(Client client)
    {
        var identityclaims = new Claim[]
        {
            new Claim("Id",client.Id.ToString()),
            new Claim("FirstName",client.FirstName),
            new Claim("LastName",client.LastName),
            new Claim(ClaimTypes.MobilePhone,client.PhoneNumber)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ea6fdb8a - 2196 - 4bb0 - 8a6a - 388f39d0f1d4"));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = 24;
        var token = new JwtSecurityToken(
            issuer: "https://burgerber",
            audience: "Burgerber",
            claims: identityclaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
