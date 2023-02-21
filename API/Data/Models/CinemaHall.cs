namespace FilmFlow.API.Data.Models
{
    public class CinemaHall : Entity
    {
        public int SeatsPerRow { get; set; }

        public int RowsTotal { get; set; }

        public bool IsThreeDimensional { get; set; }

        public bool IsWheelchairFriendly { get; set; }

        public CinemaHall()
        {
        }

        public CinemaHall(int seatsPerRow, int rowsTotal, bool isThreeDimensional, bool isWheelchairFriendly)
        {
            SeatsPerRow = seatsPerRow;
            RowsTotal = rowsTotal;
            IsThreeDimensional = isThreeDimensional;
            IsWheelchairFriendly = isWheelchairFriendly;
        }
    }
}
