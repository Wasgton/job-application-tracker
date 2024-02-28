using System.Data;
using Dapper;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Domain.Entity.Job;
using JobApplicationTracker.Domain.Enum;
using JobApplicationTracker.domain.repository;
using JobApplicationTracker.Infra.Database.Contexts;
using Microsoft.IdentityModel.Tokens;

namespace JobApplicationTracker.infra.database.Sqlserver;

public class MssqlDapperJobRepository : IJobRepository
{
    private readonly IDbConnection _connection = new DapperDbContext().Connection;

    public List<Job> GetAll()
    {
        var result = _connection.Query("SELECT * FROM Job WHERE deleted_at is null");
        List<Job> jobs = new List<Job>();
        foreach (var data in result)
        {
            Job job = new Job(
                url: data.url,
                applicationDate: data.application_date,
                role: data.role,
                requirements: data.requirements,
                benefits: data.benefits,
                status: (JobStatusEnum) data.status,
                company: data.company,
                salary: data.salary,
                responseStatus: data.response_status,
                responseDate: data.response_date,
                archived: data.archived,
                id: data.id,
                deletedAt: data.deleted_at
            );
            jobs.Add(job);
        }
        return jobs;
    }

    public Job GetById(string id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@id", id);
        var result = _connection.QuerySingle("SELECT * FROM Job WHERE deleted_at is null AND id = @id", parameters);
        if(result == null) throw new NotFoundException("Job not found");
        return new Job(
            result.url,
            result.application_date,
            result.role,
            result.requirements,
            result.benefits,
            (JobStatusEnum) result.status,
            result.company,
            result.salary,
            result.response_status,
            result.response_date,
            result.archived,
            result.id,
            result.deleted_at 
        );
    }

    public Guid Create(Job job)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Url", job.Url);
        parameters.Add("@ApplicationDate", job.ApplicationDate);
        parameters.Add("@Role", job.Role);
        parameters.Add("@Requirements", job.Requirements);
        parameters.Add("@Benefits", job.Benefits);
        parameters.Add("@Company", job.Company);
        parameters.Add("@Salary", job.Salary);
        parameters.Add("@ResponseStatus", job.ResponseStatus);
        parameters.Add("@ResponseDate", job.ResponseDate);
        parameters.Add("@Status", job.status);
        parameters.Add("@Archived", job.Archived??false);
        parameters.Add("@DeletedAt", job.DeletedAt);
        
        return _connection.QuerySingle<Guid>(@"
            INSERT INTO Job (url,application_date,role,requirements,benefits,company,salary,response_status,response_date,status,archived,deleted_at)
            OUTPUT INSERTED.Id
            VALUES (@Url,@ApplicationDate,@Role,@Requirements,@Benefits,@Company,@Salary,@ResponseStatus,@ResponseDate,@Status,@Archived,@DeletedAt);
            SELECT SCOPE_IDENTITY();"
            ,parameters);
    }

    public void Update(Job job)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Url", job.Url);
        parameters.Add("@ApplicationDate", job.ApplicationDate);
        parameters.Add("@Role", job.Role);
        parameters.Add("@Requirements", job.Requirements);
        parameters.Add("@Benefits", job.Benefits);
        parameters.Add("@Company", job.Company);
        parameters.Add("@Salary", job.Salary);
        parameters.Add("@ResponseStatus", job.ResponseStatus);
        parameters.Add("@ResponseDate", job.ResponseDate);
        parameters.Add("@Status", job.status);
        parameters.Add("@Archived", job.Archived??false);
        parameters.Add("@DeletedAt", job.DeletedAt);
        parameters.Add("@Id", job.Id);
        
        _connection.Execute(@"
            UPDATE Job 
            SET
                url = @Url,
                application_date = @ApplicationDate,
                role = @Role,
                requirements = @Requirements,
                benefits = @Benefits,
                company = @Company,
                salary = @Salary,
                response_status = @ResponseStatus,
                response_date = @ResponseDate,
                status = @Status,
                archived = @Archived,
                deleted_at = @DeletedAt 
            WHERE
                deleted_at is null 
                AND id = @Id
        ", parameters);
    }

    public void Delete(Job job)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@id", job.Id);
        if (!job.Id.HasValue) throw new Exception("Job not found");
        _connection.Execute("UPDATE Job SET deleted_at = GETDATE() WHERE id = @id", parameters);
    }

}