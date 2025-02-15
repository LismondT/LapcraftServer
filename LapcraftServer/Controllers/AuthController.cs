using Microsoft.AspNetCore.Mvc;

using LapcraftServer.Api.Contracts.Auth;
using LapcraftServer.Application.Interfaces.Auth;
using LapcraftServer.Application.DTOs.Auth;

namespace LapcraftServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
	{
        AccessAndRefreshTokens? success = await _authService.Register(new RegisterDto(
			request.Username,
			request.Email,
			request.Password));

		if (success == null)
		{
			return BadRequest("Registration was failed. Try again");
		}

		Response.Cookies.Append("syndetheite", success.AccessToken);

		return Ok(success);
	}


	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
	{
        AccessAndRefreshTokens? success = await _authService.Login(new LoginDto(
			request.Username,
			request.Password));

		if (success == null)
		{
			return BadRequest("User not founded. Password or username is incorrect"); 
		}
		
		//from Greek "syndetheite" is "log in"
		Response.Cookies.Append("syndetheite", success.AccessToken);

		return Ok(success);
	}


	[HttpPost("refresh")]
	public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
	{
        AccessAndRefreshTokens? success = await _authService.RefreshToken(new RefreshTokenDto(
			request.RefreshToken));

		if (success == null)
		{
			return BadRequest("Refresh token is exired");
		}

		Response.Cookies.Append("syndetheite", success.AccessToken);

		return Ok(success);
	}

}
