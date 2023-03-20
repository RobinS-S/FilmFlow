namespace FilmFlow.Shared.Dto
{
    public class CinemaShowDto
    {
        public long Id { get; set; }

        public MovieDto? Movie { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public long MovieId { get; set; }

        public long CinemaHallId { get; set; }

        public bool IsSecret { get; set; }
    }
}
