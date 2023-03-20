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
        private readonly ShowTicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly ReservationService _reservationService;
        private readonly CinemaHallService _cinemaHallService;

        public TicketController(ShowTicketService ticketService, IMapper mapper, ReservationService reservationService, CinemaHallService cinemaHallService)
        {
            this._ticketService = ticketService;
            this._mapper = mapper;
            this._reservationService = reservationService;
            this._cinemaHallService = cinemaHallService;
        }

        [HttpGet("byCode")]
        [ProducesResponseType(typeof(ShowTicketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCode([FromQuery] string code)
        {
            var ticket = await _ticketService.GetByCode(code);
            if (ticket == null)
            {
                return NotFound();
            }

            var ticketDto = _mapper.Map<ShowTicketDto>(ticket);
            return Ok(ticketDto);
        }

        [HttpGet("qrByCode")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQrByCode([FromQuery] long reservationId, [FromQuery] string code)
        {
            var ticket = await _ticketService.GetByCode(code);
            if (ticket == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetById(reservationId);
            var seat = reservation!.ReservedSeats.Single(rs => rs.Ticket!.Code == code);
            var hall = await _cinemaHallService.GetById(reservation!.CinemaShow.CinemaHallId);

            var imageData = await QrCodeEncoding.GenerateTicket(ticket.Code,
                $"FilmFlow: {reservation.CinemaShow.Movie!.Title}", $"{reservation.CinemaShow.Start.ToShortDateString()} {reservation.CinemaShow.Start.ToShortTimeString()} - {reservation.CinemaShow.End.ToShortTimeString()}",
                $"Hall {reservation!.CinemaShow.CinemaHall.Id}, row {hall!.Rows.Single(hr => hr.Id == seat.Seat.ParentRowId).RowId}, seat {seat.Seat.SeatNumber} {(hall.IsThreeDimensional ? "3D" : "")}",
                "Enjoy the show!");
            return File(imageData, "image/png");
        }
    }
}
