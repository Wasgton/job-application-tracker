using System.ComponentModel.DataAnnotations;
using JobApplicationTracker.Domain.Enum;

namespace JobApplicationTracker.Application.Dto;

public class JobUpdateInput
{
    [Required] 
    public string Id { get; set; }
    [Required]
    public string Url { get; set; } = string.Empty;
    [Required]
    public DateTime ApplicationDate { get; set; }
    [Required]
    public string Role { get; set; } = string.Empty;
    [Required]
    public string Requirements { get; set; } = string.Empty;
    [Required]
    public string Benefits { get; set; } = string.Empty;
    [Required]
    public JobStatusEnum Status { get; set; }
    [Required]
    public string? Company { get; set; }
    public decimal? Salary { get; set; }
    public bool? ResponseStatus { get; set; } = false;
    public DateTime? ResponseDate { get; set; }
}