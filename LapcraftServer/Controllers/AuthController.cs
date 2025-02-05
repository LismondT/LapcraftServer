using Microsoft.AspNetCore.Mvc;

using LapcraftServer.Api.Contracts.Auth;
using LapcraftServer.Application.DTOs;
using LapcraftServer.Application.Interfaces.Auth;

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
		await _authService.Register(new RegisterDto() {
			Username = request.Username,
			Email = request.Email,
			Password = request.Password,
		});

		return Ok();
	}


	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
	{
        string token = await _authService.Login(new LoginDto() {
			 Username = request.Username,
			 Password = request.Password,
		});

		if (string.IsNullOrEmpty(token))
		{
			return BadRequest("User not founded"); 
		}
		
		//from Greek "syndetheite" is "log in"
		Response.Cookies.Append("syndetheite", token);

		return Ok(token);
	}

}
