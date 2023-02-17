namespace FilmFlow.Server.Data.Models
{
    public class ShowTicket : Entity
    {
        public string Code { get; set; } = null!;

        public CinemaShow CinemaShow { get; set; } = null!;

        public ShowTicket() { }

        public ShowTicket(CinemaShow cinemaShow)
        {
            Code = Misc.Crypto.GenerateHash(Misc.Crypto.GenerateRandomBaseEncodedString());
            CinemaShow = cinemaShow;
        }
    }
}
