using LapcraftServer.Application.DTOs;

namespace LapcraftServer.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<string> Login(LoginDto loginDto);
    Task<bool> Register(RegisterDto registerDto);
}
