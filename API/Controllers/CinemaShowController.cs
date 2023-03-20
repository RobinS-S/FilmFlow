using AutoMapper;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/cinemashows")]
    public class CinemaShowController : ControllerBase
    {
        private readonly CinemaShowService _cinemaShowService;
        private readonly ReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CinemaShowController(CinemaShowService cinemaShowService, ReservationService reservationService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._cinemaShowService = cinemaShowService;
            this._reservationService = reservationService;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var cinemaShow = await _cinemaShowService.GetById(id);

            if (cinemaShow == null)
            {
                return NotFound();
            }

            var cinemaHallDto = _mapper.Map<CinemaShowDto>(cinemaShow);
            return Ok(cinemaHallDto);
        }

        [HttpGet("{id}/reserved")]
        [ProducesResponseType(typeof(List<ReservationSeatDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservedSeatsByShowId(long id)
        {
            var seats = await _reservationService.GetReservedSeatsForCinemaShow(id);

            var cinemaHallDto = _mapper.Map<IEnumerable<ReservationSeatDto>>(seats);
            return Ok(cinemaHallDto);
        }

        [HttpPost("{id}/reservation")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize]
        public async Task<IActionResult> CreateReservationForShow(long id, [FromBody] CreateReservationDto reservationDto)
        {
            var cinemaShow = await _cinemaShowService.GetById(id);

            if (cinemaShow == null)
            {
                return NotFound();
            }

            var user = User.Identity?.Name != null ? await _userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            reservationDto.CinemaShowId = id;
            var reservation = await _reservationService.Create(reservationDto, user);
            if(reservation == null)
            {
                return Conflict();
            }

            var createdReservation = _mapper.Map<ReservationDto>(reservation);
            return Ok(createdReservation);
        }

        [HttpGet("byStartEnd")]
        [ProducesResponseType(typeof(List<CinemaShowDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByStartEndDate([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var shows = await _cinemaShowService.GetByStartEndDate(start, end);

            var showsThisWeek = _mapper.Map<List<CinemaShowDto>>(shows);
            return Ok(showsThisWeek);
        }
    }
}
