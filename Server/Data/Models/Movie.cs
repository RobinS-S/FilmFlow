namespace FilmFlow.Server.Data.Models
{
    public class Movie : Entity
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set;}

        public string Category { get; set; }  = null!;

        public ICollection<CinemaShow> CinemaShows { get; set; } = new HashSet<CinemaShow>();

        public Movie() { }

        public Movie(string title, string description, DateTime releaseDate, string category)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            Category = category;
        }
    }
}
