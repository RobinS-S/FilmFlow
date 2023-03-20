using FilmFlow.API.Data.Entities.Helpers;

namespace FilmFlow.API.Data.Entities
{
    public class CinemaHall : Entity
    {
        public bool IsThreeDimensional { get; set; }

        public bool IsWheelchairFriendly { get; set; }

        public ICollection<CinemaHallRow> Rows { get; set; } = new HashSet<CinemaHallRow>();

        public CinemaHall()
        {
        }

        public CinemaHall(bool isThreeDimensional, bool isWheelchairFriendly)
        {
            IsThreeDimensional = isThreeDimensional;
            IsWheelchairFriendly = isWheelchairFriendly;
        }

        public CinemaHall(ICollection<CinemaHallRow> rows, bool isThreeDimensional, bool isWheelchairFriendly) : this(isThreeDimensional, isWheelchairFriendly)
        {
            Rows = rows;
        }
    }
}
