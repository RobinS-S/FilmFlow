﻿using FilmFlow.Domain.Entities.Helpers;

namespace FilmFlow.Domain.Entities
{
    public class Reservation : Entity
    {
        public string Code { get; set; } = null!;

        public CinemaShow CinemaShow { get; set; } = null!;

        public long CinemaShowId { get; set; }

        public ApplicationUser? User { get; set; }

        public string? UserId { get; set; }

        public bool IsPaid { get; set; }

        public ICollection<ReservationSeat> ReservedSeats { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public Reservation() { }

        public Reservation(CinemaShow cinemaShow, ICollection<ReservationSeat> reservedSeats, bool isPaid = false, ApplicationUser? user = null)
        {
            Code = Crypto.GenerateHash(Crypto.GenerateRandomBaseEncodedString());
            CreatedAt = DateTime.UtcNow;
            CinemaShow = cinemaShow;
            ReservedSeats = reservedSeats;
            IsPaid = isPaid;
            User = user;
        }
    }
}
