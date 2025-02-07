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
        string salt = _passwordHasherService.GenerateSalt();
        string passwordWithSalt = registerDto.Password + salt;
        string passwordHash = _passwordHasherService.Generate(passwordWithSalt);

        User user = User.Create(
            username: registerDto.Username,
            email: registerDto.Email,
            passwordHash: passwordHash,
            passwordSalt: salt,
            isAdmin: false
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

        bool result = _passwordHasherService.Verify(loginDto.Password, user.PasswordHash, user.PasswordSalt);
        
        if (result == false)
        {
            return string.Empty;
        }

        string jwtToken = _jwtService.GenerateToken(user);

        return jwtToken;
    }
}
