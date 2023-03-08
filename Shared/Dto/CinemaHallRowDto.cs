namespace FilmFlow.Shared.Dto
{
    public class CinemaHallRowDto
    {
        public long Id { get; set; }

        public ICollection<CinemaHallRowSeatDto> Seats { get; set; } = null!;

        public int RowId { get; set; }

        public long HallId { get; set; }
    }
}