using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LapcraftServer.Domain.Interfaces;
using LapcraftServer.Domain.Entities;

namespace LapcraftServer.Api.Controllers;

[Authorize (Policy = "AdminPolicy")]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;


    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        IEnumerable<User> users = await _userRepository.GetAllUsers();

        return Ok(users);
    }
}
