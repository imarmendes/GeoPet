using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using GeoPet.Interfaces.Services;
using System.Text;
using System.Security.Claims;
using GeoPet.DataContract.Model;

namespace GeoPet.Service;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Owner user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = AddClaims(user),
            SigningCredentials = credentials,
            Expires = DateTime.Now.AddDays(1)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity AddClaims(Owner user)
    {
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        claimsIdentity.AddClaim(new Claim("userId", user.Id.ToString()));

        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        Thread.CurrentPrincipal = claimsPrincipal;

        return claimsIdentity;
    }
}

