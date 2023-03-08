using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using FilmFlow.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext context;

        public ReservationService(ApplicationDbContext dbContext, CinemaHallService cinemaHallService, CinemaHallRowService cinemaHallRowService, CinemaShowService cinemaShowService, MovieService movieService)
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

        public async Task<List<ReservationSeat>> GetReservedSeatsForCinemaShow(long cinemaShowId)
        {
            return await context.Reservations
                .Where(r => r.CinemaShowId == cinemaShowId)
                .Include(r => r.ReservedSeats)
                .SelectMany(r => r.ReservedSeats)
                .ToListAsync();
        }

        public async Task<Reservation?> Create(CreateReservationDto reservationDto, ApplicationUser? user = null)
        {
            // retrieve movie, decide tarriff, check if seat not taken, place reservation
            var existingReservation = await context.Reservations.Include(r => r.ReservedSeats)
                .SingleOrDefaultAsync(r => r.CinemaShowId == reservationDto.CinemaShowId && r.ReservedSeats.Any(rs => reservationDto.Seats.Any(s => s.CinemaHallRowId == rs.Seat.ParentRowId && s.SeatNumber == rs.Seat.SeatNumber))); 
            if (existingReservation != null) return null;

            var cinemaShow = await context.CinemaShows.
                Include(cs => cs.Movie)
                .SingleOrDefaultAsync(cs => cs.Id == reservationDto.CinemaShowId);
            if (cinemaShow == null) return null;

            var seats = await context.CinemaHallRowSeats.Where(chrs => reservationDto.Seats.Any(s => chrs.SeatNumber == s.SeatNumber && chrs.ParentRowId == s.CinemaHallRowId))
                .ToListAsync();

            var reservation = new Reservation(cinemaShow, reservationDto.Seats.Select(s => new ReservationSeat(seats.Single(fs => fs.SeatNumber == s.SeatNumber && fs.ParentRowId == s.CinemaHallRowId), s.Tarriff)).ToList(), false, user);
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
            return reservation;
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