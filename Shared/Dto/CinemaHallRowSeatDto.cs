namespace FilmFlow.Shared.Dto
{
    public class CinemaHallRowSeatDto
    {
        public long Id { get; set; }

        public long ParentRowId { get; set; }

        public int SeatNumber { get; set; }
    }
}