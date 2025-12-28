using APILevelizd.DTO.Request;
using APILevelizd.Models;
using AutoMapper;

namespace APILevelizd.DTO.Mappings;

public class ReviewDTOMappingProfile : Profile
{
    public ReviewDTOMappingProfile()
    {
        CreateMap<Review, CreateReviewDTO>().ReverseMap();
    }
}

