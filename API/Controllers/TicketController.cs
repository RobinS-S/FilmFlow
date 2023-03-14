using AutoMapper;
using FilmFlow.API.Misc;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ShowTicketService ticketService;
        private readonly IMapper mapper;
        private readonly ReservationService reservationService;
        private readonly CinemaHallService cinemaHallService;

        public TicketController(ShowTicketService ticketService, IMapper mapper, ReservationService reservationService, CinemaHallService cinemaHallService)
        {
            this.ticketService = ticketService;
            this.mapper = mapper;
            this.reservationService = reservationService;
            this.cinemaHallService = cinemaHallService;
        }

        [HttpGet("byCode")]
        [ProducesResponseType(typeof(ShowTicketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCode([FromQuery] string code)
        {
            var ticket = await ticketService.GetByCode(code);
            if(ticket == null)
            {
                return NotFound();
            }

            var ticketDto = mapper.Map<ShowTicketDto>(ticket);
            return Ok(ticketDto);
		}

		[HttpGet("qrByCode")]
		[ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetQrByCode([FromQuery] long reservationId, [FromQuery] string code)
        {
            /*
            string title = "FilmFlow: The Lion King";
            string time = "14-03-2023 15:00 - 17:00";
            string subtitle = "Zaal 5, Rij 5, stoel 8 (3D Bril nodig!)";
            string bodyText = "Veel plezier!";*/

            var ticket = await ticketService.GetByCode(code);
			if (ticket == null)
			{
				return NotFound();
			}

            var reservation = await reservationService.GetById(reservationId);
            var seat = reservation!.ReservedSeats.Single(rs => rs.Ticket!.Code == code);
            var hall = await cinemaHallService.GetById(reservation!.CinemaShow.CinemaHallId);

			byte[] imageData = await QrCodeEncoding.GenerateTicket(ticket.Code, 
                $"FilmFlow: {reservation.CinemaShow.Movie!.Title}", $"{reservation.CinemaShow.Start.ToShortDateString()} {reservation.CinemaShow.Start.ToShortTimeString()} - {reservation.CinemaShow.End.ToShortTimeString()}",
                $"Hall {reservation!.CinemaShow.CinemaHall.Id}, row {hall!.Rows.Single(hr => hr.Id == seat.Seat.ParentRowId).RowId}, seat {seat.Seat.SeatNumber} {(hall.IsThreeDimensional ? "3D" : "")}",
                "Enjoy the show!");
			return File(imageData, "image/png");
		}
	}
}
