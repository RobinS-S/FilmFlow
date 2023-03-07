using FilmFlow.API.Data.Entities.Helpers;

namespace FilmFlow.API.Data.Entities
{
    public class ShowTicket : Entity
    {
        public string Code { get; set; } = null!;

        public string SoldBy { get; set; } = null!;

        public ReservationSeat Seat { get; set; } = null!;

        public ShowTicket() { }

        public ShowTicket(ReservationSeat seat, string soldBy)
        {
            Code = Misc.Crypto.GenerateHash(Misc.Crypto.GenerateRandomBaseEncodedString());
            Seat = seat;
            SoldBy = soldBy;
        }
    }
}
