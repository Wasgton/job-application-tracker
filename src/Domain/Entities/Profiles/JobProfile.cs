using AutoMapper;
using JobApplicationTracker.Application.Dto;

namespace JobApplicationTracker.Domain.Entities.Profiles;

public class JobProfile : Profile
{
    public JobProfile()
    {
        CreateMap<Job, JobInput>();
        CreateMap<Job, JobOutput>();
        CreateMap<JobInput, Job>();
        CreateMap<JobOutput, Job>();
        CreateMap<JobUpdateInput, Job>();
    }
}