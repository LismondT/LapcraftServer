using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using LapcraftServer.Domain.Entities;
using LapcraftServer.Application.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;

namespace LapcraftServer.Infastructure.Services.Auth;

public class JwtService(IConfiguration configuration) : IJwtService
{
	private readonly IConfiguration _configuration = configuration;

    public string GenerateToken(User user)
    {
		IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
		string keyValue = jwtSettings["Key"] ?? throw new Exception("Key value for jwt token was not founded");
		SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(keyValue));
		SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

		List<Claim> claims = [
			new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
		];

		if (user.IsAdmin)
		{
			claims.Add(new Claim("Admin", "true"));
		}

		JwtSecurityToken token = new(
			issuer: jwtSettings["Issuer"],
			audience: jwtSettings["Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),
			signingCredentials: credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
