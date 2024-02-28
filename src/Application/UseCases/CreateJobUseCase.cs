
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Domain.Entity.Job;
using JobApplicationTracker.Domain.Enum;
using JobApplicationTracker.domain.repository;
using JobApplicationTracker.infra.database.Sqlserver;
using Microsoft.OpenApi.Extensions;

namespace JobApplicationTracker.Application.UseCases;

public class CreateJobUseCase(IJobRepository? repository = null)
{
    private IJobRepository _repository = repository?? new MssqlDapperJobRepository();

    public JobOutput Execute(JobInput jobData)
    {
        Job newJob = new Job(
            jobData.Url,
            jobData.ApplicationDate,
            jobData.Role,
            jobData.Requirements,
            jobData.Benefits,
            jobData.Status,
            jobData.Company,
            jobData.Salary,
            jobData.ResponseStatus,
            jobData.ResponseDate
        );
        Guid id = _repository.Create(newJob);
        var output = _repository.GetById(id.ToString());
        return new JobOutput{
            Id = id.ToString(),
            Url = output.Url,
            ApplicationDate = output.ApplicationDate,
            Role = output.Role,
            Requirements = output.Requirements,
            Benefits = output.Benefits,
            Status = output.status.GetDisplayName(),
            Company = output.Company,
            Salary = output.Salary,
            ResponseStatus = output.ResponseStatus,
            ResponseDate = output.ResponseDate,
            Archived = output.Archived,
            DeletedAt = output.DeletedAt
        };
    }
    
}