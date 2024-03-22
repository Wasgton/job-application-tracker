using AutoMapper;
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities;
using JobApplicationTracker.Domain.Entities.Profiles;
using JobApplicationTracker.infra.database.Sqlserver;
using JobApplicationTracker.logs;

namespace JobApplicationTracker.Application.UseCases.UserUseCases;

public interface IRegisterUserUseCase
{
    public RegisterOutput Execute(RegisterInput input);
}

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private IUserRepository _repository;
    private IMapper _mapper;
    private int _iterations = 3;
    private string _salt = Password.GenerateSalt();
    private string? pepper;

    public RegisterUserUseCase(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        pepper = configuration.GetSection("PasswordPepper").Value;
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<UserProfile>(); });
        _mapper = config.CreateMapper();
    }
    
    public RegisterOutput Execute(RegisterInput input)
    {
        if (input.Password != input.ConfirmPassword)
            throw new ArgumentException("Password and confirmation doesn't match!");
        var userDb = _repository.GetByUserName(input.Username);
        System.Diagnostics.Debug.WriteLine(userDb);
        if (userDb != null)
            throw new AlreadyRegisteredException("User already exists");
        input.Password = Password.ComputeHash(input.Password, _salt, pepper!, _iterations);
        User userInput = _mapper.Map<User>(input);
        userInput.Salt = _salt;
        Guid id = _repository.Create(userInput);
        User? user = _repository.GetById(id);
        if(user == null) throw new NotFoundException("User not found");
        return _mapper.Map<RegisterOutput>(user);
    }
}