using APILevelizd.Models;
using AutoMapper;

namespace APILevelizd.DTO.Mappings;

public class GameDTOMappingProfile : Profile
{
    public GameDTOMappingProfile()
    {
        CreateMap<Game, GameDTO>().ReverseMap();
    }
}
