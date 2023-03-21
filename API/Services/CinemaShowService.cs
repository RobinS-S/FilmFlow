using FilmFlow.API.Data;
using FilmFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class CinemaShowService
    {
        private readonly ApplicationDbContext _context;

        public CinemaShowService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<CinemaShow>> GetAll()
        {
            return await _context.CinemaShows.ToListAsync();
        }

        public async Task<CinemaShow?> GetById(long id)
        {
            return await _context.CinemaShows.FindAsync(id);
        }

        public async Task<List<CinemaShow>> GetByMovieId(long movieId)
        {
            return await _context.CinemaShows
                .Where(cs => cs.MovieId == movieId)
                .ToListAsync();
        }

        public async Task Create(CinemaShow cinemaShow)
        {
            await _context.CinemaShows.AddAsync(cinemaShow);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<CinemaShow> cinemaShows)
        {
            await _context.CinemaShows.AddRangeAsync(cinemaShows);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CinemaShow cinemaShow)
        {
            _context.CinemaShows.Update(cinemaShow);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var cinemaShow = await _context.CinemaShows.FindAsync(id);
            if (cinemaShow == null) return false;
            _context.CinemaShows.Remove(cinemaShow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CinemaShow>> GetByStartEndDate(DateTime start, DateTime end)
        {
            return await _context.CinemaShows
                .Where(cs => cs.Start >= start && cs.End <= end)
                .OrderBy(cs => cs.Start)
                .Include(cs => cs.Movie)
                .ToListAsync();
        }
    }
}