using FilmFlow.API.Data.Entities.Helpers;

namespace FilmFlow.API.Data.Entities
{
    public class CinemaHallRow : Entity
    {
        public int RowChairsTotal { get; set; }

        public int RowId { get; set; }

        public CinemaHall Hall { get; set; } = null!;

        public long HallId { get; set; }

        public CinemaHallRow()
        {
        }

        public CinemaHallRow(int rowChairsTotal, int rowId)
        {
            RowChairsTotal = rowChairsTotal;
            RowId = rowId;
        }
    }
}
