using FilmFlow.API.Data.Entities.Helpers;
using FilmFlow.API.Data.Enums;

namespace FilmFlow.API.Data.Entities
{
    public class Reservation : Entity
    {
        public string Code { get; set; } = null!;

        public CinemaShow CinemaShow { get; set; } = null!;

        public long CinemaShowId { get; set; }

        public ApplicationUser? User { get; set; }

        public string? UserId { get; set; }

        public ShowTicket? Ticket { get; set; }

        public long? TicketId { get; set; }

        public bool IsPaid { get; set; }

        public TarriffType TarriffType { get; set; }

        public int SeatId { get; set; }

        public CinemaHallRow Row { get; set; } = null!;

        public long RowId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Reservation() { }

        public Reservation(CinemaShow cinemaShow, CinemaHallRow row, ApplicationUser? user, bool isPaid, TarriffType tarriffType, int seatId)
        {
            Code = Misc.Crypto.GenerateHash(Misc.Crypto.GenerateRandomBaseEncodedString());
            Row = row;
            CinemaShow = cinemaShow;
            User = user;
            IsPaid = isPaid;
            TarriffType = tarriffType;
            SeatId = seatId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
