namespace JobApplicationTracker.Application.Dto;

public class JobOutput
{
    public required string Id { get; set; }
    public required string Url { get; set; }
    public required DateTime ApplicationDate { get; set; }
    public required string Role { get; set; }
    public required string Requirements { get; set; }
    public required string Benefits { get; set; }
    public required string Status { get; set; }
    public required string? Company { get; set; }
    public required decimal? Salary { get; set; }
    public required bool? ResponseStatus { get; set; }
    public required DateTime? ResponseDate { get; set; }
    public required bool? Archived { get; set; }
    public required DateTime? DeletedAt { get; set; }
}