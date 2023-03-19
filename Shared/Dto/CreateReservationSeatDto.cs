using FilmFlow.Shared.Enums;

namespace FilmFlow.Shared.Dto
{
    public class CreateReservationSeatDto
    {
        public long SeatId { get; set; }

        public TariffType Tariff { get; set; }
    }
}
