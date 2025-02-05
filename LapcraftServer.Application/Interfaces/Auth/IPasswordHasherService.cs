﻿namespace LapcraftServer.Application.Interfaces.Auth;

public interface IPasswordHasherService
{
    public string GenerateSalt();
    public string Generate(string password);
    public bool Verify(string password, string hashedPassword, string salt);
}
