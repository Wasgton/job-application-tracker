using AutoMapper;
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities;
using JobApplicationTracker.Domain.Entities.Profiles;

namespace JobApplicationTracker.Application.UseCases.UserUseCases;

public interface ILoginUserUseCase
{
    public LoginOutput Execute(LoginInput input);
}

public class LoginUseCase : ILoginUserUseCase
{
    private IUserRepository _repository;
    private IMapper _mapper;
    private int _iterations = 3;
    private string _salt = Password.GenerateSalt();
    private string? _pepper;

    public LoginUseCase(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _pepper = configuration.GetSection("PasswordPepper").Value;
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<UserProfile>(); });
        _mapper = config.CreateMapper();
    }
    
    public LoginOutput Execute(LoginInput input)
    {
        var user = _repository.GetByUserName(input.Username);
        if (user is null)
            throw new LoginException("Incorrect Username, user not found");
        var passwordHash = Password.ComputeHash(input.Password, user.Salt, _pepper, _iterations);
        if (passwordHash != user.Hash)
            throw new LoginException("Incorrect Password");
        return _mapper.Map<LoginOutput>(user);
    }
}