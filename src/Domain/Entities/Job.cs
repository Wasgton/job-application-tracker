
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Domain.Enum;

namespace JobApplicationTracker.Domain.Entities;

public class Job
{
    [MaxLength(36)] 
    public Guid? Id { get; private set; }
    
    [MaxLength(255)]    
    [Column("url")] 
    public string Url { get; private set; }
    
    [Column("application_date")] 
    public DateTime ApplicationDate { get; private set; }
    
    [MaxLength(255)]    
    [Column("role")] 
    public string Role { get; private set; }
    
    [Column("requirements")]
    public string Requirements { get; private set; }
    
    [Column("benefits")] 
    public string Benefits { get; private set; }
    
    [Column("status")] 
    public JobStatusEnum Status { get; private set; }
    
    [MaxLength(255)] 
    [Column("company")] 
    public string? Company { get; private set; }
    
    [Column("salary")] 
    public decimal? Salary { get; private set; }
    
    [Column("response_status")] 
    public bool? ResponseStatus { get; private set; } = false;
    
    [Column("response_date")] 
    public DateTime? ResponseDate { get; private set; }
    
    [Column("archived")] 
    public bool? Archived { get; private set; }
    
    [Column("deleted_at")] 
    public DateTime? DeletedAt { get; private set; }

    public Job(
        string url,
        DateTime applicationDate,
        string role,
        string requirements,
        string benefits,
        JobStatusEnum status = JobStatusEnum.Pending,
        string? company = null,
        decimal? salary = null,
        bool? responseStatus = null,
        DateTime? responseDate = null,
        bool? archived = false,
        Guid? id = null,
        DateTime? deletedAt = null
    )
    {
        Url = url;
        ApplicationDate = applicationDate;
        Role = role;
        Requirements = requirements;
        Benefits = benefits;
        Status = status;
        if (id.HasValue) Id = id;
        if (!company.IsNullOrEmpty()) Company = company;
        if (salary.HasValue) Salary = salary;
        if (responseStatus.HasValue) ResponseStatus = responseStatus;
        if (responseDate.HasValue && ResponseStatus.Equals(true)) ResponseDate = responseDate;
        if (archived.HasValue) Archived = archived;
        if (deletedAt.HasValue) DeletedAt = deletedAt;
        _validate();
    }
    
    private void _validate()
    {
        if (Url.IsNullOrEmpty()) throw new ValidationException("url is required");
        if (Role.IsNullOrEmpty()) throw new EntityValidationException("role is required");
        if (Requirements.IsNullOrEmpty()) throw new EntityValidationException("requirements is required");
        if (Benefits.IsNullOrEmpty()) throw new EntityValidationException("benefits is required");
        if (!System.Enum.IsDefined(typeof(JobStatusEnum), Status))
            throw new EntityValidationException("Status is not valid");
    }

    public void GenerateId()
    {
        Id = Guid.NewGuid();
    }

    public void Archive()
    {
        Archived = true;
    }
    
    public void UnArchive()
    {
        Archived = false;
    }
    
    public void Restore()
    {
        DeletedAt = null;
    }
}