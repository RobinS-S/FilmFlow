using FilmFlow.API.Data.Models.Helpers;

namespace FilmFlow.API.Data.Models
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

        public CinemaHall(List<CinemaHallRow> rows, bool isThreeDimensional, bool isWheelchairFriendly) : this(isThreeDimensional, isWheelchairFriendly)
        {
            Rows = rows;
        }
    }
}
