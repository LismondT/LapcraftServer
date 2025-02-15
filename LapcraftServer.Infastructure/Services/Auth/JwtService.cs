using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using LapcraftServer.Domain.Entities.Auth;
using LapcraftServer.Domain.Entities;
using LapcraftServer.Application.Interfaces.Auth;

namespace LapcraftServer.Infastructure.Services.Auth;

public class JwtService(IConfiguration configuration) : IJwtService
{
	private readonly IConfiguration _configuration = configuration;

    public Task<string> GenerateAccessToken(User user)
    {
		IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
		string keyValue = jwtSettings["Key"]
            ?? throw new Exception("Key value for jwt token was not founded");
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

		return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public Task<RefreshToken> GenerateRefreshToken()
    {
		IConfigurationSection refreshSettings = _configuration.GetSection("RefreshToken");
		double expiry = Convert.ToDouble(refreshSettings["ExpiryInMinutes"]
            ?? throw new Exception("ExpiryInMinutes for refresh token was not founded"));

		byte[] refreshBytes = new byte[32];
		using RandomNumberGenerator rand = RandomNumberGenerator.Create();
		rand.GetBytes(refreshBytes);

		string token = Convert.ToHexString(refreshBytes);
		DateTime expired = DateTime.UtcNow.AddMinutes(expiry);

		return Task.FromResult(RefreshToken.Create(token, expired));
	}
}
