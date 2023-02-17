namespace FilmFlow.Server.Data.Models
{
    public class ShowTicket : Entity
    {
        public string Code { get; set; } = null!;

        public string SoldBy { get; set; } = null!;

        public Reservation Reservation { get; set; } = null!;

        public ShowTicket() { }

        public ShowTicket(string soldBy)
        {
            Code = Misc.Crypto.GenerateHash(Misc.Crypto.GenerateRandomBaseEncodedString());
            SoldBy = soldBy;
        }
    }
}
