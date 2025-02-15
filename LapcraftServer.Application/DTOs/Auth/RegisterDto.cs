using System.ComponentModel.DataAnnotations;

namespace LapcraftServer.Application.DTOs.Auth;

public record RegisterDto(
    [Required] string Username,
    [Required] string Email,
    [Required] string Password);
