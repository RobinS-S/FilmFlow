namespace FilmFlow.Server.Data.Models
{
    public class MovieReview : Entity
    {
        public int Stars { get; set; }

        public Movie Movie { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Text { get; set; } = null!;

        public MovieReview() { }

        public MovieReview(Movie movie, int stars, string author, string text)
        {
            Stars = stars;
            Movie = movie;
            Author = author;
            Text = text;
        }
    }
}
