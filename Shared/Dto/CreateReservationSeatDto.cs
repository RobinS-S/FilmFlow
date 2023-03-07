using FilmFlow.Shared.Enums;

namespace FilmFlow.Shared.Dto
{
    public class CreateReservationSeatDto
    {
        public long CinemaHallRowId { get; set; }

        public int SeatNumber { get; set; }

        public TarriffType Tarriff { get; set; }
    }
}
