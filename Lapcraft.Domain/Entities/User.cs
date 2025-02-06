namespace LapcraftServer.Domain.Entities;

public record User(Guid Id, string Username, string Email, string PasswordHash, string PasswordSalt)
{
    public static User Create(string username, string email, string passwordHash, string passwordSalt) =>
        new(Guid.NewGuid(), username, email, passwordHash, passwordSalt);
}
