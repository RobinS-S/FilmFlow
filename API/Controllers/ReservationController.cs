using AutoMapper;
using FilmFlow.Domain.Entities;
using FilmFlow.API.Services;
using FilmFlow.Application;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ReservationController(ReservationService reservationService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this._reservationService = reservationService;
            this._userManager = userManager;
            this._mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var user = User.Identity?.Name != null ? await _userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            var reservations = await _reservationService.GetByUserId(user.Id);
            var reservationDtos = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationDtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var user = User.Identity?.Name != null ? await _userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            var reservation = await _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.UserId != user.Id)
            {
                return Forbid();
            }

            var reservationDtos = _mapper.Map<ReservationDto>(reservation);
            return Ok(reservationDtos);
        }

        [Authorize]
        [HttpPost("{id}/pay")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PayReservation(long id)
        {
            var user = User.Identity?.Name != null ? await _userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            var reservation = await _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.UserId != user.Id)
            {
                return Forbid();
            }

            var paid = await _reservationService.PayReservation(reservation);
            if (!paid)
            {
                return Conflict();
            }

            return Ok(paid);
        }

        [Authorize]
        [HttpGet("{id}/qrcode")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQrCode(long id)
        {
            var user = User.Identity?.Name != null ? await _userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            var reservation = await _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.UserId != user.Id)
            {
                return Forbid();
            }

            var imageData = await QrCodeEncoding.GenerateQrCodeAsPngFromText(reservation.Code);
            return File(imageData, "image/png");
        }
    }
}
