using System.ComponentModel.DataAnnotations;

namespace LapcraftServer.Application.DTOs.Auth;

public record LoginDto(
    [Required] string Username,
    [Required] string Password);
