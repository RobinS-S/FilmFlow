using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class CinemaHallService
    {
        private readonly ApplicationDbContext _context;

        public CinemaHallService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<CinemaHall>> GetAll()
        {
            return await _context.CinemaHalls
                .Include(ch => ch.Rows)
                .ThenInclude(r => r.Seats.OrderBy(s => s.SeatNumber))
                .ToListAsync();
        }

        public async Task<CinemaHall?> GetById(long id)
        {
            return await _context.CinemaHalls
                .Include(ch => ch.Rows.OrderBy(r => r.RowId))
                .ThenInclude(r => r.Seats.OrderBy(s => s.SeatNumber))
                .SingleOrDefaultAsync(ch => ch.Id == id);
        }

        public async Task Create(CinemaHall cinemaHall)
        {
            await _context.CinemaHalls.AddAsync(cinemaHall);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<CinemaHall> cinemaHalls)
        {
            await _context.CinemaHalls.AddRangeAsync(cinemaHalls);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CinemaHall cinemaHall)
        {
            _context.CinemaHalls.Update(cinemaHall);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var cinemaHall = await _context.CinemaHalls.FindAsync(id);
            if (cinemaHall == null) return false;
            _context.CinemaHalls.Remove(cinemaHall);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}