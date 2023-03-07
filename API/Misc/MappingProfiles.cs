using AutoMapper;
using FilmFlow.API.Data.Entities;
using FilmFlow.Shared.Dto;

namespace FilmFlow.API.Misc
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MovieReview, MovieReviewDto>()
                .ReverseMap();

            CreateMap<Movie, MovieDto>()
                .ReverseMap();

            CreateMap<CinemaHall, CinemaHallDto>()
                .ReverseMap();

            CreateMap<CinemaHallRow, CinemaHallRowDto>()
                .ReverseMap();

            CreateMap<CinemaShow, CinemaShowDto>()
                .ReverseMap();

            CreateMap<ApplicationUser, UserProfileDto>();

            CreateMap<ReservationSeat, ReservationSeatDto>();
        }
    }
}
