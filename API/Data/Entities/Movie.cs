using FilmFlow.API.Data.Entities.Helpers;

namespace FilmFlow.API.Data.Entities
{
    public class Movie : Entity
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public string Category { get; set; } = null!;

        public int MinAge { get; set; }

        public string Language { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public ICollection<MovieReview> MovieReviews { get; set; } = new HashSet<MovieReview>();

        public ICollection<CinemaShow> CinemaShows { get; set; } = new HashSet<CinemaShow>();

        public Movie() { }

        public Movie(string title, string description, DateTime releaseDate, string category, int minAge, string language, string imageUrl)
        {
            Title = title;
            Description = description;
            ReleaseDate = releaseDate;
            Category = category;
            MinAge = minAge;
            Language = language;
            ImageUrl = imageUrl;
        }
    }
}
