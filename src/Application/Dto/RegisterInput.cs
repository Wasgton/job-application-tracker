using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using JobApplicationTracker.Domain.Entities;

namespace JobApplicationTracker.Application.Dto;

[AutoMap(typeof(User))]
public class RegisterInput
{
    [Required]
    public string Username { get; set; } = String.Empty;

    [Required] 
    public string Password { get; set; } = String.Empty;
    
    [Ignore]
    [Required]
    public string ConfirmPassword { get; set; } = String.Empty;
}