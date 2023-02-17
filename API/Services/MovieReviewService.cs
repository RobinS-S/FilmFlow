using FilmFlow.Server.Data.Models;
using FilmFlow.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class MovieReviewService
    {
        private readonly ApplicationDbContext context;

        public MovieReviewService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<MovieReview>> GetAll()
        {
            return await context.MovieReviews.ToListAsync();
        }

        public async Task<MovieReview?> GetById(long id)
        {
            return await context.MovieReviews.FindAsync(id);
        }

        public async Task Create(MovieReview movieReview)
        {
            context.MovieReviews.Add(movieReview);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<MovieReview> movieReviews)
        {
            context.MovieReviews.AddRange(movieReviews);
            await context.SaveChangesAsync();
        }

        public async Task Update(MovieReview movieReview)
        {
            context.MovieReviews.Update(movieReview);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var movieReview = await context.MovieReviews.FindAsync(id);
            if (movieReview == null) return false;
            context.MovieReviews.Remove(movieReview);
            await context.SaveChangesAsync();
            return true;
        }
    }
}