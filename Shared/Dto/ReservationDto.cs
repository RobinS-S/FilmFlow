namespace FilmFlow.Shared.Dto
{
    public class ReservationDto
    {
        public long Id { get; set; }

        public long CinemaShowId { get; set; }

        public ICollection<ReservationSeatDto> Seats { get; set; } = null!;
    }
}
