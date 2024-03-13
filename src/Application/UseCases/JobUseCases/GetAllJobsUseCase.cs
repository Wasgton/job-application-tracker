using AutoMapper;
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities.Profiles;
using Microsoft.OpenApi.Extensions;

namespace JobApplicationTracker.Application.UseCases.JobUseCases;

public class GetAllJobsUseCase
{
    private readonly IJobRepository _repository;
    private readonly IMapper _mapper;

    public GetAllJobsUseCase(IJobRepository repository)
    {
        _repository = repository;
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<JobProfile>(); });
        _mapper = config.CreateMapper();
    }


    public List<JobOutput> Execute()
    {
        var result = _repository.GetAll();
        return result.Select(
            job => _mapper.Map<JobOutput>(job)
        ).ToList();
    }
}