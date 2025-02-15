using LapcraftServer.Application.DTOs.Auth;

namespace LapcraftServer.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<AccessAndRefreshTokens?> Login(LoginDto loginDto);
    Task<AccessAndRefreshTokens?> Register(RegisterDto registerDto);
    Task<AccessAndRefreshTokens?> RefreshToken(RefreshTokenDto refreshTokenDto);
}
