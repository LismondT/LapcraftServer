using LapcraftServer.Domain.Entities;

namespace LapcraftServer.Domain.Interfaces;

public interface IUserRepository
{
	Task<IEnumerable<User>> GetAllUsers();
	Task<User?> GetUserByUsername(string username);
	Task AddUser(User user);
}
