using System.ComponentModel.DataAnnotations;
using LapcraftServer.Domain.Entities.Auth;

namespace LapcraftServer.Application.DTOs.Auth;

public record class AccessAndRefreshTokens(
    [Required] string AccessToken,
    [Required] RefreshToken RefreshToken
);
