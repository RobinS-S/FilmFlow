using FilmFlow.API.Data;
using FilmFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class ShowTicketService
    {
        private readonly ApplicationDbContext _context;

        public ShowTicketService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<ShowTicket>> GetAll()
        {
            return await _context.ShowTickets.ToListAsync();
        }

        public async Task<ShowTicket?> GetById(long id)
        {
            return await _context.ShowTickets.FindAsync(id);
        }

        public async Task<ShowTicket?> GetByCode(string code)
        {
            return await _context.ShowTickets.SingleOrDefaultAsync(st => st.Code == code);
        }

        public async Task Create(ShowTicket showTicket)
        {
            _context.ShowTickets.Add(showTicket);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<ShowTicket> showTickets)
        {
            _context.ShowTickets.AddRange(showTickets);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ShowTicket showTicket)
        {
            _context.ShowTickets.Update(showTicket);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var showTicket = await _context.ShowTickets.FindAsync(id);
            if (showTicket == null) return false;
            _context.ShowTickets.Remove(showTicket);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}