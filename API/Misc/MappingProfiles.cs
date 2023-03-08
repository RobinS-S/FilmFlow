using AutoMapper;
using FilmFlow.API.Data.Entities;
using FilmFlow.Shared.Dto;

namespace FilmFlow.API.Misc
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MovieReview, MovieReviewDto>();

            CreateMap<Movie, MovieDto>();

            CreateMap<CinemaHall, CinemaHallDto>();

            CreateMap<CinemaHallRowSeat, CinemaHallRowSeatDto>();

            CreateMap<CinemaHallRow, CinemaHallRowDto>();

            CreateMap<CinemaShow, CinemaShowDto>();

            CreateMap<ApplicationUser, UserProfileDto>();

            CreateMap<ReservationSeat, ReservationSeatDto>();

            CreateMap<Reservation, ReservationDto>();
        }
    }
}
