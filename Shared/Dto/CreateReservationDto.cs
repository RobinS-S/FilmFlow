namespace FilmFlow.Shared.Dto
{
    public class CreateReservationDto
    {
        public long CinemaShowId { get; set; }

        public ICollection<CreateReservationSeatDto> Seats { get; set; } = null!;
    }
}
