using FilmFlow.API.Data;
using FilmFlow.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class CinemaShowService
    {
        private readonly ApplicationDbContext context;

        public CinemaShowService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<CinemaShow>> GetAll()
        {
            return await context.CinemaShows.ToListAsync();
        }

        public async Task<CinemaShow?> GetById(long id)
        {
            return await context.CinemaShows.FindAsync(id);
        }

        public async Task Create(CinemaShow cinemaShow)
        {
            await context.CinemaShows.AddAsync(cinemaShow);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<CinemaShow> cinemaShows)
        {
            await context.CinemaShows.AddRangeAsync(cinemaShows);
            await context.SaveChangesAsync();
        }

        public async Task Update(CinemaShow cinemaShow)
        {
            context.CinemaShows.Update(cinemaShow);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var cinemaShow = await context.CinemaShows.FindAsync(id);
            if (cinemaShow == null) return false;
            context.CinemaShows.Remove(cinemaShow);
            await context.SaveChangesAsync();
            return true;
        }
    }
}