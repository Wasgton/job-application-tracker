using AutoMapper;
using JobApplicationTracker.Application.Dto;

namespace JobApplicationTracker.Domain.Entities.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, LoginInput>();
        CreateMap<LoginInput, User>();
        CreateMap<RegisterInput, User>().ForMember(
            dest=>dest.Hash, 
            option => option.MapFrom(src => src.Password));
        CreateMap<User, RegisterOutput>();
    }
}