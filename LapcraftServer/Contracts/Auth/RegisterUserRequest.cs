using System.ComponentModel.DataAnnotations;

namespace LapcraftServer.Api.Contracts.Auth;

public record RegisterUserRequest(
    [Required] string Username,
    [Required] string Email,
    [Required] string Password
);