
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.domain.repository;
using JobApplicationTracker.infra.database.Sqlserver;
using Microsoft.OpenApi.Extensions;

namespace JobApplicationTracker.Application.UseCases;

public class GetAllJobsUseCase(IJobRepository? repository = null)
{
    private readonly IJobRepository _repository = repository?? new MssqlDapperJobRepository();

    public List<JobOutput> Execute()
    {
        var result = _repository.GetAll();
        return result.Select(
            job => new JobOutput { 
                        Id = job.Id.ToString(),
                        Url = job.Url,
                        ApplicationDate = job.ApplicationDate,
                        Role = job.Role,
                        Requirements = job.Requirements,
                        Benefits = job.Benefits,
                        Status = job.status.GetDisplayName(),
                        Company = job.Company,
                        Salary = job.Salary,
                        ResponseStatus = job.ResponseStatus,
                        ResponseDate = job.ResponseDate,
                        Archived = job.Archived,
                        DeletedAt = job.DeletedAt
                    }
            ).ToList();
    }
    
}
