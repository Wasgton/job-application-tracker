using AutoMapper;
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities.Profiles;
using Microsoft.OpenApi.Extensions;

namespace JobApplicationTracker.Application.UseCases.JobUseCases;

public class GetJobByIdUseCase
{
    private readonly IJobRepository _repository;
    private readonly IMapper _mapper;

    public GetJobByIdUseCase(IJobRepository repository)
    {
        _repository = repository;
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<JobProfile>(); });
        _mapper = config.CreateMapper();
    }

    public JobOutput Execute(string id)
    {
        var result = _repository.GetById(id);
        if (result == null) throw new NotFoundException("Job not found");
        return _mapper.Map<JobOutput>(result);
    }
}