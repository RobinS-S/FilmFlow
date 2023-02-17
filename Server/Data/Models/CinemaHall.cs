namespace FilmFlow.Server.Data.Models
{
    public class CinemaHall : Entity
    {
        public int SeatsPerRow { get; set; }
        
        public int RowsTotal { get; set; }

        public CinemaHall()
        {
        }

        public CinemaHall(int seatsPerRow, int rowsTotal)
        {
            SeatsPerRow = seatsPerRow;
            RowsTotal = rowsTotal;
        }
    }
}
