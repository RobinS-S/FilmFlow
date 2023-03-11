using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class SocialService
    {
        private readonly ApplicationDbContext context;

        public SocialService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Social>> GetAll()
        {
            return await context.Socials.ToListAsync();
        }

        public async Task<Social?> GetById(long id)
        {
            return await context.Socials.FindAsync(id);
        }

        public async Task Create(Social social)
        {
            await context.Socials.AddAsync(social);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<Social> socials)
        {
            await context.Socials.AddRangeAsync(socials);
            await context.SaveChangesAsync();
        }

        public async Task Update(Social social)
        {
            context.Socials.Update(social);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var social = await context.Socials.FindAsync(id);
            if (social == null) return false;
            context.Socials.Remove(social);
            await context.SaveChangesAsync();
            return true;
        }
    }
}