using FilmFlow.API.Data;
using FilmFlow.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class ShowTicketService
    {
        private readonly ApplicationDbContext context;

        public ShowTicketService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<ShowTicket>> GetAll()
        {
            return await context.ShowTickets.ToListAsync();
        }

        public async Task<ShowTicket?> GetById(long id)
        {
            return await context.ShowTickets.FindAsync(id);
        }

        public async Task<ShowTicket?> GetByCode(string code)
        {
            return await context.ShowTickets.SingleOrDefaultAsync(st => st.Code == code);
        }

        public async Task Create(ShowTicket showTicket)
        {
            context.ShowTickets.Add(showTicket);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<ShowTicket> showTickets)
        {
            context.ShowTickets.AddRange(showTickets);
            await context.SaveChangesAsync();
        }

        public async Task Update(ShowTicket showTicket)
        {
            context.ShowTickets.Update(showTicket);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var showTicket = await context.ShowTickets.FindAsync(id);
            if (showTicket == null) return false;
            context.ShowTickets.Remove(showTicket);
            await context.SaveChangesAsync();
            return true;
        }
    }
}