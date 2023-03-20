using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class SocialService
    {
        private readonly ApplicationDbContext _context;

        public SocialService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<Social>> GetAll()
        {
            return await _context.Socials.ToListAsync();
        }

        public async Task<Social?> GetById(long id)
        {
            return await _context.Socials.FindAsync(id);
        }

        public async Task Create(Social social)
        {
            await _context.Socials.AddAsync(social);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<Social> socials)
        {
            await _context.Socials.AddRangeAsync(socials);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Social social)
        {
            _context.Socials.Update(social);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var social = await _context.Socials.FindAsync(id);
            if (social == null) return false;
            _context.Socials.Remove(social);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}