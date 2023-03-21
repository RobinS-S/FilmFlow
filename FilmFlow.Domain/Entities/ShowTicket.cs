using FilmFlow.Domain.Entities.Helpers;

namespace FilmFlow.Domain.Entities
{
    public class ShowTicket : Entity
    {
        public string Code { get; set; } = null!;

        public string SoldBy { get; set; } = null!;

        public ReservationSeat Seat { get; set; } = null!;

        public ShowTicket() { }

        public ShowTicket(ReservationSeat seat, string soldBy)
        {
            Code = Crypto.GenerateHash(Crypto.GenerateRandomBaseEncodedString());
            Seat = seat;
            SoldBy = soldBy;
        }
    }
}
