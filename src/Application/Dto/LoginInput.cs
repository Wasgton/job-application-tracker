using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Application.Dto;

public class LoginInput
{
    [Required]
    public string Email { get; set; } = String.Empty;
    [Required]
    public string Password { get; set; } = String.Empty;
}