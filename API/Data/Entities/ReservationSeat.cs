using FilmFlow.API.Data.Entities.Helpers;
using FilmFlow.Shared.Enums;

namespace FilmFlow.API.Data.Entities
{
    public class ReservationSeat : Entity
    {
        public Reservation Reservation { get; set; } = null!;

        public long ReservationId { get; set; }

        public CinemaHallRowSeat Seat { get; set; } = null!;

        public long SeatId { get; set; }

        public TarriffType TarriffType { get; set; }

        public ShowTicket? Ticket { get; set; }

        public long? TicketId { get; set; }

        public ReservationSeat(CinemaHallRowSeat seat, TarriffType tarriffType)
        {
            Seat = seat;
            TarriffType = tarriffType;
        }

        private ReservationSeat() { }
    }
}
