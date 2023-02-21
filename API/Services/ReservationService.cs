using FilmFlow.API.Data;
using FilmFlow.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext context;

        public ReservationService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Reservation>> GetAll()
        {
            return await context.Reservations.ToListAsync();
        }

        public async Task<Reservation?> GetById(long id)
        {
            return await context.Reservations.FindAsync(id);
        }

        public async Task<Reservation?> GetByCode(string code)
        {
            return await context.Reservations.SingleOrDefaultAsync(r => r.Code == code);
        }

        public async Task Create(Reservation reservation)
        {
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<Reservation> reservations)
        {
            await context.Reservations.AddRangeAsync(reservations);
            await context.SaveChangesAsync();
        }

        public async Task Update(Reservation reservation)
        {
            context.Reservations.Update(reservation);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var reservation = await context.Reservations.FindAsync(id);
            if (reservation == null) return false;
            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();
            return true;
        }
    }
}