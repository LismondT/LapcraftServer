using LapcraftServer.Application.Interfaces.Auth;

namespace LapcraftServer.Infastructure.Services.Auth;

public class PasswordHasherService : IPasswordHasherService
{
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}
