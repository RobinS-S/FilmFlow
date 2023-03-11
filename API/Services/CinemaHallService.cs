﻿using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class CinemaHallService
    {
        private readonly ApplicationDbContext context;

        public CinemaHallService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<CinemaHall>> GetAll()
        {
            return await context.CinemaHalls
                .Include(ch => ch.Rows)
                .ThenInclude(r => r.Seats.OrderBy(s => s.SeatNumber))
                .ToListAsync();
        }

        public async Task<CinemaHall?> GetById(long id)
        {
            return await context.CinemaHalls
                .Include(ch => ch.Rows.OrderBy(r => r.RowId))
                .ThenInclude(r => r.Seats.OrderBy(s => s.SeatNumber))
                .SingleOrDefaultAsync(ch => ch.Id == id);
        }

        public async Task Create(CinemaHall cinemaHall)
        {
            await context.CinemaHalls.AddAsync(cinemaHall);
            await context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<CinemaHall> cinemaHalls)
        {
            await context.CinemaHalls.AddRangeAsync(cinemaHalls);
            await context.SaveChangesAsync();
        }

        public async Task Update(CinemaHall cinemaHall)
        {
            context.CinemaHalls.Update(cinemaHall);
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var cinemaHall = await context.CinemaHalls.FindAsync(id);
            if (cinemaHall == null) return false;
            context.CinemaHalls.Remove(cinemaHall);
            await context.SaveChangesAsync();
            return true;
        }
    }
}