using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Application.UseCases.UserUseCases;
using JobApplicationTracker.logs;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Infra.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IRegisterUserUseCase _registerUserUseCase;

    public UserController(IRegisterUserUseCase registerUserUseCase)
    {
        _registerUserUseCase = registerUserUseCase;
    }
    [HttpPost("register", Name = "RegisterUser")]
    public IActionResult Register([FromBody] RegisterInput input)
    {
        try
        {
            return Ok(_registerUserUseCase.Execute(input));
        }
        catch (CustomException e)
        {
            Log.info($"{e.Message} - {e.StackTrace!.ToString()}");
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            Log.info($"{e.Message} - {e.StackTrace!.ToString()}");
            return Problem("Something went wrong", statusCode: 500);
        }
    }

}