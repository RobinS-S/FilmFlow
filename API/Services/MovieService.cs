using FilmFlow.API.Data;
using FilmFlow.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class MovieService
    {
        private readonly ApplicationDbContext context;

        public MovieService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Movie>> GetAll()
        {
            return await context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetById(long id)
        {
            return await context.Movies.FindAsync(id);
        }

        public async Task Create(Movie movie)
        {
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<Movie> movies)
        {
            await context.Movies.AddRangeAsync(movies);
            await context.SaveChangesAsync();
        }

        public async Task Update(Movie movie)
        {
            context.Movies.Update(movie);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var movie = await context.Movies.FindAsync(id);
            if (movie == null) return false;
            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
            return true;
        }
    }
}