namespace LapcraftServer.Domain.Entities;

public record User(Guid Id, string Username, string Email, string PasswordHash, string PasswordSalt)
{
    public static User Create(Guid id, string username, string email, string passwordHash, string passwordSalt) =>
        new(id, username, email, passwordHash, passwordSalt);
}
