using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Misc;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FilmFlow.API.Services
{
    public class ReservationService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;
        private readonly CinemaHallService _cinemaHallService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationService(ApplicationDbContext dbContext, EmailService emailService, CinemaHallService cinemaHallService, UserManager<ApplicationUser> userManager)
        {
            _context = dbContext;
            this._emailService = emailService;
            this._cinemaHallService = cinemaHallService;
            this._userManager = userManager;
        }

        public async Task<List<Reservation>> GetAll()
        {
            return await _context.Reservations
                .Include(r => r.CinemaShow)
                .ThenInclude(cs => cs.Movie)
                .ToListAsync();
        }

        public async Task<Reservation?> GetById(long id)
        {
            return await _context.Reservations
                .Where(r => r.Id == id)
                .Include(r => r.CinemaShow)
                .ThenInclude(cs => cs.Movie)
                .Include(r => r.ReservedSeats)
                .ThenInclude(rs => rs.Seat)
                .Include(r => r.ReservedSeats)
                .ThenInclude(rs => rs.Ticket)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Reservation>> GetByUserId(string id)
        {
            return await _context.Reservations
                .Include(r => r.CinemaShow)
                .ThenInclude(cs => cs.Movie)
                .Where(r => r.UserId != null && r.UserId == id)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByCode(string code)
        {
            return await _context.Reservations.SingleOrDefaultAsync(r => r.Code == code);
        }

        public async Task<List<ReservationSeat>> GetReservedSeatsForCinemaShow(long cinemaShowId)
        {
            return await _context.Reservations
                .Where(r => r.CinemaShowId == cinemaShowId)
                .Include(r => r.ReservedSeats)
                .ThenInclude(rs => rs.Seat)
                .SelectMany(r => r.ReservedSeats)
                .ToListAsync();
        }

        public async Task<Reservation?> Create(CreateReservationDto reservationDto, ApplicationUser? user = null)
        {
            // retrieve movie, decide tariff, check if seat not taken, place reservation
            var attemptedSeatIdsToReserve = reservationDto.Seats.Select(s => s.SeatId);
            var existingReservation = await _context.Reservations.Include(r => r.ReservedSeats)
                .SingleOrDefaultAsync(r => r.CinemaShowId == reservationDto.CinemaShowId && r.ReservedSeats.Any(rs => attemptedSeatIdsToReserve.Contains(rs.SeatId)));
            if (existingReservation != null) return null;

            var cinemaShow = await _context.CinemaShows.
                Include(cs => cs.Movie)
                .SingleOrDefaultAsync(cs => cs.Id == reservationDto.CinemaShowId);
            if (cinemaShow == null) return null;

            var seats = await _context.CinemaHallRowSeats.Where(chrs => attemptedSeatIdsToReserve.Contains(chrs.Id))
                .ToListAsync();

            var reservation = new Reservation(cinemaShow, reservationDto.Seats.Select(s => new ReservationSeat(seats.Single(fs => fs.Id == s.SeatId), s.Tariff)).ToList(), false, user);
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<ApplicationUser?> GetUserByReservationId(long id)
        {
            var reservation = await _context.Reservations.SingleAsync(r => r.Id == id);
            return await _userManager.FindByIdAsync(reservation!.UserId!);
        }

        public async Task<bool> PayReservation(Reservation reservation, string soldBy = "FilmFlow site")
        {
            if (reservation.IsPaid)
            {
                return false;
            }

            foreach (var seat in reservation.ReservedSeats)
            {
                seat.Ticket = new ShowTicket(seat, soldBy);
            }
            reservation.IsPaid = true;

            await Update(reservation);

            var user = await GetUserByReservationId(reservation.Id);
            if (user == null) throw new Exception("No user found for reservation");
            var newReservation = await GetById(reservation.Id);
            var attachments = new Dictionary<string, byte[]>();
            var hall = await _cinemaHallService.GetById(newReservation!.CinemaShow.CinemaHallId);
            foreach (var seat in newReservation!.ReservedSeats)
            {
                var row = hall!.Rows.Single(hr => hr.Id == seat.Seat.ParentRowId);
                byte[] imageData = await QrCodeEncoding.GenerateTicket(seat!.Ticket!.Code,
                    $"FilmFlow: {reservation.CinemaShow.Movie!.Title}", $"{reservation.CinemaShow.Start.ToShortDateString()} {reservation.CinemaShow.Start.ToShortTimeString()} - {reservation.CinemaShow.End.ToShortTimeString()}",
                    $"Hall {reservation!.CinemaShow.CinemaHall.Id}, row {hall!.Rows.Single(hr => hr.Id == seat.Seat.ParentRowId).RowId}, seat {seat.Seat.SeatNumber} {(hall.IsThreeDimensional ? "3D" : "")}",
                    "Enjoy the show!");
                attachments.Add($"{newReservation.CinemaShow!.Movie!.Title.Replace(" ", "").Replace(":", "")}-r{row.RowId}-s{seat.Seat.SeatNumber}", imageData);
            }
            await _emailService.SendHtmlEmailWithAttachments(user.Email!, user.UserName ?? "FilmFlow user", $"Tickets {newReservation.CinemaShow!.Movie!.Title}", "Thank you for ordering! Your tickets are attached. We hope you enjoy the show!", attachments);

            return true;
        }

        public async Task CreateRange(IEnumerable<Reservation> reservations)
        {
            await _context.Reservations.AddRangeAsync(reservations);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return false;
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}