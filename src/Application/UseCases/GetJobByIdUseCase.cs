using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.domain.repository;
using JobApplicationTracker.infra.database.Sqlserver;
using Microsoft.OpenApi.Extensions;

namespace JobApplicationTracker.Application.UseCases;

public class GetJobByIdUseCase(IJobRepository? repository = null)
{
    private readonly IJobRepository _repository = repository ?? new MssqlDapperJobRepository();

    public JobOutput Execute(string id)
    {
        var result = _repository.GetById(id);
        if (result == null) throw new NotFoundException("Job not found");
        return new JobOutput
        {
            Id = result.Id.ToString(),
            Url = result.Url,
            ApplicationDate = result.ApplicationDate,
            Role = result.Role,
            Requirements = result.Requirements,
            Benefits = result.Benefits,
            Status = result.status.GetDisplayName(),
            Company = result.Company,
            Salary = result.Salary,
            ResponseStatus = result.ResponseStatus,
            ResponseDate = result.ResponseDate,
            Archived = result.Archived,
            DeletedAt = result.DeletedAt
        };
    }
    
}
