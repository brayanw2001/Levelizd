using APILevelizd.DTO.Request;
using APILevelizd.DTO.Response;
using APILevelizd.Models;
using AutoMapper;

namespace APILevelizd.DTO.Mappings;

public class GameDTOMappingProfile : Profile
{
    public GameDTOMappingProfile()
    {
        CreateMap<CreateGameDTO, Game>();

        CreateMap<Game, ResponseGameDTO>();
    }
}
