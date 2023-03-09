using AutoMapper;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/cinemashows")]
    public class CinemaShowController : ControllerBase
    {
        private readonly CinemaShowService cinemaShowService;
        private readonly ReservationService reservationService;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public CinemaShowController(CinemaShowService cinemaShowService, ReservationService reservationService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.cinemaShowService = cinemaShowService;
            this.reservationService = reservationService;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var cinemaShow = await cinemaShowService.GetById(id);

            if (cinemaShow == null)
            {
                return NotFound();
            }

            var cinemaHallDto = mapper.Map<CinemaShowDto>(cinemaShow);
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

        [HttpPost("{id}/reservation")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize]
        public async Task<IActionResult> CreateReservationForShow(long id, [FromBody] CreateReservationDto reservationDto)
        {
            var cinemaShow = await cinemaShowService.GetById(id);

            if (cinemaShow == null)
            {
                return NotFound();
            }

            var user = User.Identity?.Name != null ? await userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            reservationDto.CinemaShowId = id;
            var reservation = await reservationService.Create(reservationDto, user);
            if(reservation == null)
            {
                return Conflict();
            }

            var createdReservation = mapper.Map<ReservationDto>(reservation);
            return Ok(createdReservation);
        }
    }

}
