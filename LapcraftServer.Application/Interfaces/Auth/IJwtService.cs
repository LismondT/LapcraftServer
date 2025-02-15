using LapcraftServer.Domain.Entities;
using LapcraftServer.Domain.Entities.Auth;

namespace LapcraftServer.Application.Interfaces.Auth;

public interface IJwtService
{
    public Task<string> GenerateAccessToken(User user);
    public Task<RefreshToken> GenerateRefreshToken();
}