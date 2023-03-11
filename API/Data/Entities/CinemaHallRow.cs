using FilmFlow.API.Data.Entities.Helpers;

namespace FilmFlow.API.Data.Entities
{
    public class CinemaHallRow : Entity
    {
        public ICollection<CinemaHallRowSeat> Seats { get; set; } = null!;

        public int RowId { get; set; }

        public CinemaHall Hall { get; set; } = null!;

        public long HallId { get; set; }

        public CinemaHallRow()
        {
        }

        public CinemaHallRow(int rowChairsTotal, int rowId)
        {
            Seats = Enumerable.Range(1, rowChairsTotal).Select(i => new CinemaHallRowSeat(i, this)).ToList();
            RowId = rowId;
        }
    }
}
