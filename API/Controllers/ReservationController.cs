using AutoMapper;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Misc;
using FilmFlow.API.Services;
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
        private readonly ReservationService reservationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ReservationController(ReservationService reservationService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.reservationService = reservationService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var user = User.Identity?.Name != null ? await userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            var reservations = await reservationService.GetByUserId(user.Id);
            var reservationDtos = mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationDtos);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var user = User.Identity?.Name != null ? await userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            var reservation = await reservationService.GetById(id);
            if(reservation == null)
            {
                return NotFound();
            }

            if(reservation.UserId != user.Id)
            {
                return Forbid();
            }

            var reservationDtos = mapper.Map<ReservationDto>(reservation);
            return Ok(reservationDtos);
        }

		[Authorize]
		[HttpPost("{id}/pay")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> PayReservation(long id)
		{
			var user = User.Identity?.Name != null ? await userManager.FindByNameAsync(User.Identity.Name) : null;
			if (user == null)
			{
				return BadRequest();
			}

			var reservation = await reservationService.GetById(id);
			if (reservation == null)
			{
				return NotFound();
			}

			if (reservation.UserId != user.Id)
			{
				return Forbid();
			}

            var paid = await reservationService.PayReservation(reservation);
            if(!paid)
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
            var user = User.Identity?.Name != null ? await userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            var reservation = await reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            if (reservation.UserId != user.Id)
            {
                return Forbid();
            }

            byte[] imageData = await QrCodeEncoding.GenerateQrCodeAsPngFromText(reservation.Code);
            return File(imageData, "image/png");
        }
    }
}
