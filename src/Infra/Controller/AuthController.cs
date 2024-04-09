using System.Security.Claims;
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Application.UseCases.AuthUseCases;
using JobApplicationTracker.Application.UseCases.UserUseCases;
using JobApplicationTracker.logs;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationTracker.Infra.Controller;

[ApiController]
[Route("/Api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _defaultRepository;
    private readonly ILoginUserUseCase _loginUseCase;
    private readonly ICreateTokenUseCase _createTokenUseCase;

    public AuthController(IUserRepository repository, ILoginUserUseCase loginUseCase, ICreateTokenUseCase createTokenUseCase)
    {
        _defaultRepository = repository;
        _loginUseCase = loginUseCase;
        _createTokenUseCase = createTokenUseCase;
    }

    [HttpPost("/create-token")]
    public IActionResult CreateToken(LoginInput input)
    {
        try
        {
            _loginUseCase.Execute(input);
            return Ok(_createTokenUseCase.Execute());
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