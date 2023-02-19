namespace FilmFlow.API.Dto
{
    public class MovieDto
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public string Category { get; set; }  = null!;

        public int MinAge { get; set; }

        public string Language { get; set; } = null!;
    }
}
