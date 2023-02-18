using AutoMapper;
using FilmFlow.Server.Data.Models;

namespace FilmFlow.API.Dto
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MovieReview, MovieReviewDto>()
                .ReverseMap();

            CreateMap<Movie, MovieDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, UserProfileDto>();
        }
    }
}
