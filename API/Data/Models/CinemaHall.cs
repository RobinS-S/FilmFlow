namespace FilmFlow.Server.Data.Models
{
    public class CinemaHall : Entity
    {
        //TODO: zaalindeling vanuit brightspace moet opgesplitst worden per rij, denk valueobject eronder
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
