using AutoMapper;
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities.Profiles;
using Microsoft.OpenApi.Extensions;

namespace JobApplicationTracker.Application.UseCases.JobUseCases;

public class GetAllDeletedJobsUseCase
{
    private readonly IJobRepository _repository;
    private readonly IMapper _mapper;

    public GetAllDeletedJobsUseCase(IJobRepository repository)
    {
        _repository = repository;
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<JobProfile>(); });
        _mapper = config.CreateMapper();
    }

    public List<JobOutput> Execute()
    {
        var result = _repository.GetAllDeleted();
        if (result.Count<1) throw new NotFoundException("No Job found");
        return result.Select(
            job => _mapper.Map<JobOutput>(job)
        ).ToList();
    }
}