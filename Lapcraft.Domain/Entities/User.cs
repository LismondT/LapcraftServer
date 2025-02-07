namespace LapcraftServer.Domain.Entities;

public record User(Guid Id, string Username, string Email, string PasswordHash, string PasswordSalt, bool IsAdmin = false)
{
    public static User Create(string username, string email, string passwordHash, string passwordSalt, bool isAdmin) =>
        new(Guid.NewGuid(), username, email, passwordHash, passwordSalt, isAdmin);
}
