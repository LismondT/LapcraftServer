using System.ComponentModel.DataAnnotations;

namespace LapcraftServer.Application.DTOs.Auth;

public record class RefreshTokenDto(
    [Required] string RefreshToken
);
