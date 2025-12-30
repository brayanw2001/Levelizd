using APILevelizd.DTO.Request;
using APILevelizd.Models;
using AutoMapper;

namespace APILevelizd.DTO.Mappings;

public class UserDTOMappingProfile : Profile
{
    public UserDTOMappingProfile()
    {
        CreateMap<User, CreateUserDTO>().ReverseMap();
    }
}
