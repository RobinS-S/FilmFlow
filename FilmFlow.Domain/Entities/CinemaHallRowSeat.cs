using FilmFlow.Domain.Entities.Helpers;

namespace FilmFlow.Domain.Entities
{
    public class CinemaHallRowSeat : Entity
    {
        public CinemaHallRow ParentRow { get; set; } = null!;

        public long ParentRowId { get; set; }

        public int SeatNumber { get; set; }

        public CinemaHallRowSeat(int seatNumber, CinemaHallRow parentRow)
        {
            SeatNumber = seatNumber;
            ParentRow = parentRow;
        }

        private CinemaHallRowSeat() { }
    }
}
