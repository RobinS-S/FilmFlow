namespace FilmFlow.Shared.Dto
{
    public class CinemaHallDto
    {
        public long Id { get; set; }

        public bool IsThreeDimensional { get; set; }

        public bool IsWheelchairFriendly { get; set; }

        public ICollection<CinemaHallRowDto> Rows { get; set; } = null!;
    }
}
