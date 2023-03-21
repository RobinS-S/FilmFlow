using FilmFlow.Domain.Entities.Helpers;
using FilmFlow.Shared.Enums;

namespace FilmFlow.Domain.Entities
{
    public class ReservationSeat : Entity
    {
        public Reservation Reservation { get; set; } = null!;

        public long ReservationId { get; set; }

        public CinemaHallRowSeat Seat { get; set; } = null!;

        public long SeatId { get; set; }

        public TariffType TariffType { get; set; }

        public ShowTicket? Ticket { get; set; }

        public long? TicketId { get; set; }

        public ReservationSeat(CinemaHallRowSeat seat, TariffType tariffType)
        {
            Seat = seat;
            TariffType = tariffType;
        }

        private ReservationSeat() { }
    }
}
