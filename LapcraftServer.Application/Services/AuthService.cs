using LapcraftServer.Application.DTOs.Auth;
using LapcraftServer.Application.Interfaces.Auth;
using LapcraftServer.Domain.Entities;
using LapcraftServer.Domain.Entities.Auth;
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

    public async Task<AccessAndRefreshTokens?> Register(RegisterDto registerDto)
    {
        string salt = _passwordHasherService.GenerateSalt();
        string passwordWithSalt = registerDto.Password + salt;
        string passwordHash = _passwordHasherService.Generate(passwordWithSalt);

        RefreshToken refreshToken = await _jwtService.GenerateRefreshToken();

        User user = User.Create(
            username: registerDto.Username,
            email: registerDto.Email,
            passwordHash: passwordHash,
            passwordSalt: salt,
            refreshToken: refreshToken
        );

        await _userRepository.AddUser(user);

        string accessToken = await _jwtService.GenerateAccessToken(user);

		return new AccessAndRefreshTokens(
            RefreshToken: refreshToken,
            AccessToken: accessToken
        );
    }


    public async Task<AccessAndRefreshTokens?> Login(LoginDto loginDto)
    {
        User? user = await _userRepository.GetByUsername(loginDto.Username);

        if (user == null)
        {
            return null;
        }

        bool result = _passwordHasherService.Verify(loginDto.Password, user.PasswordHash, user.PasswordSalt);
        
        if (result == false)
        {
            return null;
        }

        RefreshToken refreshToken = await _jwtService.GenerateRefreshToken();
        string accessToken = await _jwtService.GenerateAccessToken(user);

        await _userRepository.SetRefreshTokenById(user.Id, refreshToken)
;
        return new AccessAndRefreshTokens(
            RefreshToken: refreshToken,
            AccessToken: accessToken
        );
    }

    public async Task<AccessAndRefreshTokens?> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        User? user = await _userRepository.GetByRefreshToken(refreshTokenDto.RefreshToken);   

        if (user == null)
        {
            return null;
        }

        string accessToken = await _jwtService.GenerateAccessToken(user);
        RefreshToken refreshToken = await _jwtService.GenerateRefreshToken();

        await _userRepository.SetRefreshTokenById(user.Id, refreshToken);

        return new AccessAndRefreshTokens(
            AccessToken: accessToken,
            RefreshToken: refreshToken
        );
    }
}
