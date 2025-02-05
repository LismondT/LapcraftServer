using LapcraftServer.Domain.Entities;

namespace LapcraftServer.Application.Interfaces.Auth;

public interface IJwtService
{
    public string GenerateToken(User user);
}
