using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class CinemaHallRowService
    {
        private readonly ApplicationDbContext _context;

        public CinemaHallRowService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<CinemaHallRow>> GetAllByHallId(long hallId)
        {
            return await _context.CinemaHallRows
                .Where(chr => chr.HallId == hallId)
                .ToListAsync();
        }

        public async Task<CinemaHallRow?> GetByIdAndHallId(long hallId, long id)
        {
            return await _context.CinemaHallRows
                .SingleOrDefaultAsync(chr => chr.Id == id && chr.HallId == hallId);
        }

        public async Task Create(CinemaHallRow cinemaHallRow)
        {
            await _context.CinemaHallRows.AddAsync(cinemaHallRow);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<CinemaHallRow> cinemaHallRows)
        {
            await _context.CinemaHallRows.AddRangeAsync(cinemaHallRows);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CinemaHallRow cinemaHallRow)
        {
            _context.CinemaHallRows.Update(cinemaHallRow);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long hallId, long id)
        {
            var cinemaHallRow = await _context.CinemaHallRows.SingleOrDefaultAsync(chr => chr.HallId == hallId && chr.Id == id);
            if (cinemaHallRow == null) return false;
            _context.CinemaHallRows.Remove(cinemaHallRow);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}