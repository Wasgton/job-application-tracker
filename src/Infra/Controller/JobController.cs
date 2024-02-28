using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.UseCases;
using JobApplicationTracker.Domain.Entity.Job;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Infra.Controller;

[ApiController]
[Route("/Api/[controller]")]
public class JobController : ControllerBase
{
    [HttpGet("", Name = "GetAllJobs")]
    public IActionResult GetAll()
    {
        try {
            var getJobsUseCase = new GetAllJobsUseCase();
            return Ok(getJobsUseCase.Execute());
        } catch (CustomException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpGet("{id}", Name = "GetJobById")]
    public IActionResult Get(string id)
    {
        try {
            var getJobsUseCase = new GetJobByIdUseCase();
            return Ok(getJobsUseCase.Execute(id));
        } catch (CustomException e) {
            return BadRequest(e.Message);
        } 
        // catch (Exception e) {
        //     return Problem("Something went wrong", statusCode: 500);
        // }
    }
    
    [HttpPost("create", Name = "CreateJob")]
    public IActionResult Create([FromBody] JobInput jobInput)
    {
        try {
            var createJobUseCase = new CreateJobUseCase();
            return Ok(createJobUseCase.Execute(jobInput));
        } catch (CustomException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return Problem("Something went wrong", statusCode: 500);
        }
    }
    
    [HttpPut("update/{id}", Name = "UpdateJob")]
    public IActionResult Update(string id, [FromBody] JobInput newJob)
    {
        try {
            var getJobsUseCase = new UpdateJobUseCase();
            return Ok(getJobsUseCase.Execute(id, newJob));
        } catch (CustomException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return Problem("Something went wrong", statusCode: 500);
        }
    }
    
    [HttpPost("archive/{id}", Name = "ArchiveJob")]
    public IActionResult Archive(string id)
    {
        try {
            var getJobsUseCase = new ArchiveJobUseCase();
            getJobsUseCase.Execute(id);
            return Ok("Job Archived");
        } catch (CustomException e) {
            return BadRequest(e.Message);
        } catch (Exception e) {
            return Problem("Something went wrong", statusCode: 500);
        }
    }

    [HttpDelete("delete/{id}", Name = "DeleteJob")]
    public IActionResult Delete(string id)
    {
        try
        {
            var getJobsUseCase = new DeleteJobUseCase();
            getJobsUseCase.Execute(id);
            return Ok("Job deleted");
        }
        catch (CustomException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
            // return Problem("Something went wrong", statusCode: 500);
        }
        
    }
}