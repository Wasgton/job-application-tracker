using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Application.Dto;

public class LoginInput
{
    [Required]
    public string Username { get; set; } = String.Empty;
    [Required]
    public string Password { get; set; } = String.Empty;
}