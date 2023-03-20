using FilmFlow.Shared.Enums;

namespace FilmFlow.Shared.Dto
{
    public class ReservationSeatDto
    {
        public long Id { get; set; }

        public long ReservationId { get; set; }

        public CinemaHallRowSeatDto Seat { get; set; } = null!;

        public long SeatId { get; set; }

        public TariffType TariffType { get; set; }

        public ShowTicketDto? Ticket { get; set; }

        public long? TicketId { get; set; }
    }
}
