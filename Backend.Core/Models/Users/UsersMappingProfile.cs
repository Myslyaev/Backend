using AutoMapper;
using Backend.Core.DTOs;

namespace Backend.Core.Models.Users;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<UserDto, UserResponse>();

        CreateMap<UserDto, UserWithDevicesResponse>();

        CreateMap<CreateUserRequest, UserDto>();

        CreateMap<UpdateUserRequest, UserDto>();

        CreateMap<LoginUserRequest, UserDto>();
    }
}
