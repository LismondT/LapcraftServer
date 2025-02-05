using LapcraftServer.Application.DTOs;
using LapcraftServer.Application.Interfaces.Auth;
using LapcraftServer.Domain.Entities;
using LapcraftServer.Domain.Interfaces;

namespace LapcraftServer.Application.Services;

public class AuthService(
    IUserRepository userRepository,
    IPasswordHasherService passwordHasherService,
    IJwtService jwtService) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IPasswordHasherService _passwordHasherService = passwordHasherService;

    public async Task<bool> Register(RegisterDto registerDto)
    {
        string passwordHash = _passwordHasherService.Generate(registerDto.Password);

        User user = User.Create(
            Guid.NewGuid(),
            registerDto.Username,
            registerDto.Email,
            passwordHash
        );

        await _userRepository.AddUser(user);

		return true;
    }


    public async Task<string> Login(LoginDto loginDto)
    {
        User? user = await _userRepository.GetUserByUsername(loginDto.Username);

        if (user == null)
        {
            return string.Empty;
        }

        bool result = _passwordHasherService.Verify(loginDto.Password, user.PasswordHash);

        if (result == false)
        {
            return string.Empty;
        }

        string jwtToken = _jwtService.GenerateToken(user);

        return jwtToken;
    }
}
