using FilmFlow.API.Data;
using FilmFlow.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class CinemaHallService
    {
        private readonly ApplicationDbContext context;

        public CinemaHallService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<CinemaHall>> GetAll()
        {
            return await context.CinemaHalls.ToListAsync();
        }

        public async Task<CinemaHall?> GetById(long id)
        {
            return await context.CinemaHalls.FindAsync(id);
        }

        public async Task Create(CinemaHall cinemaHall)
        {
            await context.CinemaHalls.AddAsync(cinemaHall);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<CinemaHall> cinemaHalls)
        {
            await context.CinemaHalls.AddRangeAsync(cinemaHalls);
            await context.SaveChangesAsync();
        }

        public async Task Update(CinemaHall cinemaHall)
        {
            context.CinemaHalls.Update(cinemaHall);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var cinemaHall = await context.CinemaHalls.FindAsync(id);
            if (cinemaHall == null) return false;
            context.CinemaHalls.Remove(cinemaHall);
            await context.SaveChangesAsync();
            return true;
        }
    }
}