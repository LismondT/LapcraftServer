using System.ComponentModel.DataAnnotations;

namespace LapcraftServer.Api.Contracts.Auth;

public record class LoginUserRequest(
    [Required] string Username,
    [Required] string Password
);
