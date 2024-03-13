using System.Runtime.CompilerServices;
using AutoMapper;
using JobApplicationTracker.Application.Dto;
using JobApplicationTracker.Application.Exception;
using JobApplicationTracker.Application.Repository;
using JobApplicationTracker.Domain.Entities.Profiles;

namespace JobApplicationTracker.Application.UseCases.JobUseCases;

public class UpdateJobUseCase
{
    private readonly IMapper _mapper;
    private readonly IJobRepository _repository;

    public UpdateJobUseCase(IJobRepository repository)
    {
        _repository = repository;
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<JobProfile>(); });
        _mapper = config.CreateMapper();
    }

    public JobOutput Execute(string id, JobUpdateInput newJobData)
    {
        newJobData.Id = id;
        var jobDb = _repository.GetById(id);
        if (jobDb == null) throw new NotFoundException("Job not found");
        var jobToUpdate = _mapper.Map(newJobData, jobDb);
        _repository.Update(jobToUpdate);
        var result = _repository.GetById(id);
        if (result == null) throw new NotFoundException("Job not found");
        return _mapper.Map<JobOutput>(result);
    }
}