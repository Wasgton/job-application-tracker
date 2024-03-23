using System.ComponentModel.DataAnnotations;

namespace JobApplicationTracker.Application.Dto;

public class LoginOutput
{
    [Required]
    public string Id { get; set; } = String.Empty;
    [Required]
    public string Username { get; set; } = String.Empty;
}