using AutoMapper;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/cinemashows")]
    public class CinemaShowController : ControllerBase
    {
        private readonly CinemaShowService cinemaShowService;
        private readonly ReservationService reservationService;
        private readonly IMapper mapper;

        public CinemaShowController(CinemaShowService cinemaShowService, ReservationService reservationService, IMapper mapper)
        {
            this.cinemaShowService = cinemaShowService;
            this.reservationService = reservationService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var cinemaHall = await cinemaShowService.GetById(id);

            if (cinemaHall == null)
            {
                return NotFound();
            }

            var cinemaHallDto = mapper.Map<CinemaShowDto>(cinemaHall);
            return Ok(cinemaHallDto);
        }

        [HttpGet("{id}/reserved")]
        [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservedSeatsByShowId(long id)
        {
            var seats = await reservationService.GetReservedSeatsForCinemaShow(id);

            if (seats == null)
            {
                return NotFound();
            }

            var cinemaHallDto = mapper.Map<IEnumerable<ReservationSeatDto>>(seats);
            return Ok(cinemaHallDto);
        }
    }

}
