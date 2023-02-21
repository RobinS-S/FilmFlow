namespace FilmFlow.API.Data.Models
{
    public class CinemaShow : Entity
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Movie Movie { get; set; } = null!;

        public CinemaHall CinemaHall { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

        public CinemaShow() { }

        public CinemaShow(DateTime start, DateTime end, Movie movie, CinemaHall hall)
        {
            Start = start;
            End = end;
            Movie = movie;
            CinemaHall = hall;
        }
    }
}
