using LapcraftServer.Domain.Entities;
using LapcraftServer.Domain.Interfaces;

namespace LapcraftServer.Persistens.Repositories;

//TODO: Implement sqlite
public class UserRepository : IUserRepository
{
	private static readonly IDictionary<string, User> _users = new Dictionary<string, User>();

	public async Task AddUser(User user)
	{
		_users[user.Username] = user;
	}

	public async Task<User?> GetUserByUsername(string username)
	{
		return _users.TryGetValue(username, out User? user) ? user : null;
	}

	public async Task<IEnumerable<User>> GetAllUsers()
	{
		return _users.Values;
	}
}
