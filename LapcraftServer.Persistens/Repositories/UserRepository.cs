using Microsoft.EntityFrameworkCore;

using LapcraftServer.Domain.Entities;
using LapcraftServer.Domain.Interfaces;
using LapcraftServer.Domain.Entities.Auth;

namespace LapcraftServer.Persistens.Repositories;

public class UserRepository(LapcraftDbContext context) : IUserRepository
{
	private readonly LapcraftDbContext _context = context;

    public async Task<IEnumerable<User>> GetAll() =>
		await _context.Users.AsNoTracking().ToListAsync();

    public async Task<User?> GetByUsername(string username) =>
		await _context.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Username == username);

    public async Task<User?> GetByRefreshToken(string refreshToken) =>
		await _context.Users
			.AsNoTracking()
			.Include(x => x.RefreshToken)
			.FirstOrDefaultAsync(x => x.RefreshToken!.Token == refreshToken);

    public async Task AddUser(User user)
	{
		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
	}

	public async Task SetRefreshTokenById(Guid id, RefreshToken token)
	{
        User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
			?? throw new InvalidOperationException("Can not set refresh token to not existing user");

		User updatedUser = user with { RefreshToken = token };
		
		_context.Users.Update(updatedUser);
		await _context.SaveChangesAsync();
    }
}
