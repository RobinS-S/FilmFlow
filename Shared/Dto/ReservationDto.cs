namespace FilmFlow.Shared.Dto
{
    public class ReservationDto
    {
        public long Id { get; set; }

        public long CinemaShowId { get; set; }

        public CinemaShowDto? CinemaShow { get; set; }

        public ICollection<ReservationSeatDto> ReservedSeats { get; set; } = null!;

        public string Code { get; set; } = null!;

        public bool IsPaid { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
