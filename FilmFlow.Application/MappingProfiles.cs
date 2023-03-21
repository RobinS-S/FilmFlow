using AutoMapper;
using FilmFlow.Domain.Entities;
using FilmFlow.Shared.Dto;

namespace FilmFlow.Application
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

            CreateMap<ShowTicket, ShowTicketDto>();

            CreateMap<ReservationSeat, ReservationSeatDto>();

            CreateMap<Social, SocialDto>();

            CreateMap<Reservation, ReservationDto>();
        }
    }
}
