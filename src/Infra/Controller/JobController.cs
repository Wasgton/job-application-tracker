using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Application.UseCases.JobUseCases;
using JobApplicationTracker.infra.database.Sqlserver;
using JobApplicationTracker.logs;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Infra.Controller;

[ApiController]
[Route("/Api/[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobRepository _defaultRepository;

    public JobController(IJobRepository repository)
    {
        _defaultRepository = repository;
    }
    
    [HttpGet("", Name = "GetAllJobs")]
    public IActionResult GetAll()
    {
        try
        {
            var getJobsUseCase = new GetAllJobsUseCase(_defaultRepository);
            return Ok(getJobsUseCase.Execute());
        }
        catch (CustomException e)
        {
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpGet("{id}", Name = "GetJobById")]
    public IActionResult Get(string id)
    {
        try
        {
            var getJobsUseCase = new GetJobByIdUseCase(_defaultRepository);
            return Ok(getJobsUseCase.Execute(id));
        }
        catch (CustomException e)
        {
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpPost("create", Name = "CreateJob")]
    public IActionResult Create([FromBody] JobInput jobInput)
    {
        try
        {
            var createJobUseCase = new CreateJobUseCase(_defaultRepository);
            return Ok(createJobUseCase.Execute(jobInput));
        }
        catch (CustomException e)
        {
            Log.info(e.Message);
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Log.info(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpPut("update/{id}", Name = "UpdateJob")]
    public IActionResult Update(string id, [FromBody] JobUpdateInput newJob)
    {
        try
        {
            var getJobsUseCase = new UpdateJobUseCase(_defaultRepository);
            return Ok(getJobsUseCase.Execute(id, newJob));
        }
        catch (CustomException e)
        {
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpPost("archive/{id}", Name = "ArchiveJob")]
    public IActionResult Archive(string id)
    {
        try
        {
            var getJobsUseCase = new ArchiveJobUseCase(_defaultRepository);
            getJobsUseCase.Execute(id);
            return Ok("Job Archived");
        }
        catch (CustomException e)
        {
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpPost("unarchive/{id}", Name = "UnarchiveJob")]
    public IActionResult Unarchive(string id)
    {
        try
        {
            var getJobsUseCase = new UnarchiveJobUseCase(_defaultRepository);
            getJobsUseCase.Execute(id);
            return Ok("Job Unarchived");
        }
        catch (CustomException e)
        {
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpDelete("delete/{id}", Name = "DeleteJob")]
    public IActionResult Delete(string id)
    {
        try
        {
            var getJobsUseCase = new DeleteJobUseCase(_defaultRepository);
            getJobsUseCase.Execute(id);
            return Ok("Job deleted");
        }
        catch (CustomException e)
        {
            Log.info(e.Message);
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem(e.Message);
            // return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpPost("restore/{id}", Name = "RestoreJob")]
    public IActionResult Restore(string id)
    {
        try
        {
            var getJobsUseCase = new RestoreJobUseCase(_defaultRepository);
            getJobsUseCase.Execute(id);
            return Ok("Job Restored");
        }
        catch (CustomException e)
        {
            Log.info(e.Message);
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem(e.Message);
            // return Problem("Something went wrong", statusCode: 500);
        }
    }
    
    [HttpGet("deleted", Name = "GetAllDeletedJobs")]
    public IActionResult GetAllDeleted()
    {
        try
        {
            var getJobsUseCase = new GetAllDeletedJobsUseCase(_defaultRepository);
            return Ok(getJobsUseCase.Execute());
        }
        catch (CustomException e)
        {
            Log.info(e.StackTrace!.ToString());
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Log.info(e.StackTrace!.ToString());
            return Problem("Something went wrong", statusCode: 500);
        }
    }
}