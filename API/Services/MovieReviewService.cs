using FilmFlow.API.Data;
using FilmFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class MovieReviewService
    {
        private readonly ApplicationDbContext _context;

        public MovieReviewService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<MovieReview>> GetAllByMovieId(long movieId)
        {
            return await _context.MovieReviews
                .Where(mr => mr.MovieId == movieId)
                .Include(mr => mr.User)
                .ToListAsync();
        }

        public async Task<List<MovieReview>> GetAll()
        {
            return await _context.MovieReviews
                .Include(mr => mr.User)
                .ToListAsync();
        }

        public async Task<MovieReview?> GetById(long id)
        {
            return await _context.MovieReviews
                .Include(mr => mr.User)
                .FirstOrDefaultAsync(mr => mr.Id == id);
        }

        public async Task<MovieReview?> GetByIdForMovie(long id, long movieId)
        {
            return await _context.MovieReviews
                .Include(mr => mr.User)
                .SingleOrDefaultAsync(mr => mr.MovieId == movieId && mr.Id == id);
        }

        public async Task Create(MovieReview movieReview, ApplicationUser user)
        {
            movieReview.User = user;
            _context.MovieReviews.Add(movieReview);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<MovieReview> movieReviews)
        {
            _context.MovieReviews.AddRange(movieReviews);
            await _context.SaveChangesAsync();
        }

        public async Task Update(MovieReview movieReview)
        {
            _context.MovieReviews.Update(movieReview);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var movieReview = await _context.MovieReviews.FindAsync(id);
            if (movieReview == null) return false;
            _context.MovieReviews.Remove(movieReview);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}