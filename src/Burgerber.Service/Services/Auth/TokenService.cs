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
        this._config= configuration.GetSection("Jwt");
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = int.Parse(_config["LifeTime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issure"],
            audience: _config["Audience"],
            claims: identityclaims,
            expires:TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}
