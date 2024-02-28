using JobApplicationTracker.Domain.Enum;

namespace JobApplicationTracker.Application.Dto;

public class JobInput
{
    public required string Url {get; set;}
    public required DateTime ApplicationDate {get; set;}
    public required string Role {get; set;}
    public required string Requirements {get; set;}
    
    ///teste
    
    public required string Benefits {get; set;}
    public required JobStatusEnum Status { get; set;}
    
    public string? Company {get; set;}
    public decimal? Salary {get; set;}
    public bool? ResponseStatus {get; set;} = false;
    public DateTime? ResponseDate {get; set;}
    
}