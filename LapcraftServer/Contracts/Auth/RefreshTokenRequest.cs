using System.ComponentModel.DataAnnotations;

namespace LapcraftServer.Api.Contracts.Auth;

public record RefreshTokenRequest(
    [Required] string RefreshToken
);
