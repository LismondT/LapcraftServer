using System.Text.Json.Serialization;

using LapcraftServer.Domain.Entities.Auth;

namespace LapcraftServer.Domain.Entities;

public record User(
    Guid Id,
    string Username,
    string Email,
    [property: JsonIgnore] string PasswordHash,
    [property: JsonIgnore] string PasswordSalt,
    [property: JsonIgnore] RefreshToken? RefreshToken = null,
    bool IsAdmin = false)
{
    /// <summary>
    /// For EF
    /// </summary>
    private User() : this(Guid.NewGuid(), "", "", "", "", null, false) {}

    public static User Create(
        string username,
        string email,
        string passwordHash,
        string passwordSalt,
        RefreshToken refreshToken) =>
        new(Guid.NewGuid(), username, email, passwordHash, passwordSalt, refreshToken);
}
