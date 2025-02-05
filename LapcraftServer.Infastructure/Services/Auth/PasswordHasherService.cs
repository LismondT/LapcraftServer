using LapcraftServer.Application.Interfaces.Auth;

namespace LapcraftServer.Infastructure.Services.Auth;

public class PasswordHasherService : IPasswordHasherService
{
    public string GenerateSalt() =>
        BCrypt.Net.BCrypt.GenerateSalt();
    
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public bool Verify(string password, string hashedPassword, string salt) =>
        BCrypt.Net.BCrypt.Verify(password + salt, hashedPassword);
}
