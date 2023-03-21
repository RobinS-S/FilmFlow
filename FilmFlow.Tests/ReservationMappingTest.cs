using FilmFlow.Domain.Entities;
using FilmFlow.Shared.Dto;

namespace FilmFlow.Tests
{
    public class ReservationMappingTest
    {
        [Fact]
        public void TestReservationPropertyMappingTest()
        {
	        var mapper = Mapper.CreateMockMapper();
	        
	        var movie = new Movie("Inception",
		        "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
		        DateTime.Now.AddYears(-10), "Sci-Fi", 12, "English", "https://url.img/test.png");

	        var movieDto = mapper.Map<MovieDto>(movie);

            Assert.Equal(movie.Title, movieDto.Title);
            Assert.Equal(movie.Description, movieDto.Description);
            Assert.Equal(movie.ReleaseDate, movieDto.ReleaseDate);
            Assert.Equal(movie.Category, movieDto.Category);
            Assert.Equal(movie.MinAge, movieDto.MinAge);
            Assert.Equal(movie.Language, movieDto.Language);
            Assert.Equal(movie.ImageUrl, movieDto.ImageUrl);
		}
    }
}
