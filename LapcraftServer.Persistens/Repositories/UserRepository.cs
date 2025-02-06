using Microsoft.EntityFrameworkCore;

using LapcraftServer.Domain.Entities;
using LapcraftServer.Domain.Interfaces;

namespace LapcraftServer.Persistens.Repositories;

//TODO: Implement sqlite
public class UserRepository(LapcraftDbContext context) : IUserRepository
{
	private readonly LapcraftDbContext _context = context;

    public async Task AddUser(User user)
	{
		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
	}

	public async Task<User?> GetUserByUsername(string username)
	{
		return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
	}

	public async Task<IEnumerable<User>> GetAllUsers()
	{
		return await _context.Users.AsNoTracking().ToListAsync();
	}
}
