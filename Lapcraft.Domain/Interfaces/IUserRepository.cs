using LapcraftServer.Domain.Entities;
using LapcraftServer.Domain.Entities.Auth;

namespace LapcraftServer.Domain.Interfaces;

public interface IUserRepository
{
	Task<IEnumerable<User>> GetAll();
	Task<User?> GetByUsername(string username);
	Task<User?> GetByRefreshToken(string refreshToken);
	Task SetRefreshTokenById(Guid id, RefreshToken refreshToken);
	Task AddUser(User user);
}
