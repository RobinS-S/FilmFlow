﻿using FilmFlow.Server.Data.Enums;

namespace FilmFlow.Server.Data.Models
{
    public class Reservation : Entity
    {
        public string Code { get; set; } = null!;

        public CinemaShow CinemaShow { get; set; } = null!;
        
        public ApplicationUser? User { get; set; }

        public bool IsPaid { get; set; }

        public TarriffType TarriffType { get; set; }

        public int SeatId { get; set; }

        public int RowId { get; set; }

        public Reservation() { }

        public Reservation(CinemaShow cinemaShow, ApplicationUser? user, bool isPaid, int seatId, int rowId, TarriffType tarriffType)
        {
            Code = Misc.Crypto.GenerateHash(Misc.Crypto.GenerateRandomBaseEncodedString());
            CinemaShow = cinemaShow;
            User = user;
            IsPaid = isPaid;
            SeatId = seatId;
            RowId = rowId;
            TarriffType = TarriffType;
        }
    }
}
