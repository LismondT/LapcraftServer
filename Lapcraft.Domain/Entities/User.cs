namespace LapcraftServer.Domain.Entities;

public record User(Guid Id, string Username, string Email, string PasswordHash)
{
    public static User Create(Guid id, string username, string email, string passwordHash) =>
        new User(id, username, email, passwordHash);
}
