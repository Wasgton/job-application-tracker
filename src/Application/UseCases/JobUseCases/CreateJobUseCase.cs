using AutoMapper;
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities;
using JobApplicationTracker.Domain.Entities.Profiles;
using Microsoft.OpenApi.Extensions;

namespace JobApplicationTracker.Application.UseCases.JobUseCases;

public class CreateJobUseCase
{
    private readonly IMapper _mapper;
    private readonly IJobRepository _repository;

    public CreateJobUseCase(IJobRepository repository)
    {
        _repository = repository;
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<JobProfile>(); });
        _mapper = config.CreateMapper();
    }
    public JobOutput Execute(JobInput jobData)
    {
        Job newJob = _mapper.Map<Job>(jobData);
        var id = _repository.Create(newJob);
        Job? output = _repository.GetById(id.ToString());
        if (output==null) throw new NotFoundException("Job not found");
        return _mapper.Map<JobOutput>(output);
    }
}