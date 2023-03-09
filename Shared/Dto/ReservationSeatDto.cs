namespace FilmFlow.Shared.Dto
{
    public class ReservationSeatDto
    {
        public long Id { get; set; }

        public CinemaHallRowSeatDto Seat { get; set; } = null!;

        public long SeatId { get; set; }
    }
}
