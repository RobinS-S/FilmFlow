namespace FilmFlow.Server.Data.Models
{
    public class MovieReview : Entity
    {
        public int Stars { get; set; }

        public Movie Movie { get; set; } = null!;

        public long MovieId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string Text { get; set; } = null!;

        public MovieReview() { }

        public MovieReview(Movie movie, int stars, ApplicationUser user, string text)
        {
            Stars = stars;
            Movie = movie;
            Text = text;
            User = user;
        }
    }
}
