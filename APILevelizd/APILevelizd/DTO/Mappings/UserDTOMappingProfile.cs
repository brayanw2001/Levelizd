using APILevelizd.Models;
using AutoMapper;

namespace APILevelizd.DTO.Mappings;

public class UserDTOMappingProfile : Profile
{
    public UserDTOMappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}
