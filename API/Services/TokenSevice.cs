using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace API.Servives;

public class TokenSevice : ITokenService
{
    // Egy kulcsos titkosítás belső használatra. 
    private readonly SymmetricSecurityKey _key;
    
    public TokenSevice(IConfiguration config)
    {
        //_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:TokenKey").Value));
    }
    
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),   // Token tárgy (kibocsátó adatok)
            Expires = DateTime.Now.AddDays(7),      // érvényessége
            SigningCredentials = creds              // Token aláírás (kód)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}