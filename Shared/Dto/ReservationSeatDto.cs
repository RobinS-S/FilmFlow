namespace FilmFlow.Shared.Dto
{
    public class ReservationSeatDto
    {
        public long Id { get; set; }

        public long CinemaHallRowId { get; set; }

        public int SeatNumber { get; set; }
    }
}
