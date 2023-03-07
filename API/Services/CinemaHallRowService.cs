using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class CinemaHallRowService
    {
        private readonly ApplicationDbContext context;

        public CinemaHallRowService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<CinemaHallRow>> GetAllByHallId(long hallId)
        {
            return await context.CinemaHallRows
                .Where(chr => chr.HallId == hallId)
                .ToListAsync();
        }

        public async Task<CinemaHallRow?> GetByIdAndHallId(long hallId, long id)
        {
            return await context.CinemaHallRows
                .SingleOrDefaultAsync(chr => chr.Id == id && chr.HallId == hallId);
        }

        public async Task Create(CinemaHallRow cinemaHallRow)
        {
            await context.CinemaHallRows.AddAsync(cinemaHallRow);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<CinemaHallRow> cinemaHallRows)
        {
            await context.CinemaHallRows.AddRangeAsync(cinemaHallRows);
            await context.SaveChangesAsync();
        }

        public async Task Update(CinemaHallRow cinemaHallRow)
        {
            context.CinemaHallRows.Update(cinemaHallRow);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long hallId, long id)
        {
            var cinemaHallRow = await context.CinemaHallRows.SingleOrDefaultAsync(chr => chr.HallId == hallId && chr.Id == id);
            if (cinemaHallRow == null) return false;
            context.CinemaHallRows.Remove(cinemaHallRow);
            await context.SaveChangesAsync();
            return true;
        }
    }
}