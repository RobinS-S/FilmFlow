namespace FilmFlow.API.Dto
{
    public class MovieReviewDto
    {
        public long Id { get; set; }

        public long MovieId { get; set; }

        public UserProfileDto? User { get; set; }

        public int Stars { get; set; }

        public string Text { get; set; } = null!;
    }
}
