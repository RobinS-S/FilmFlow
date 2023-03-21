using FilmFlow.Domain.Entities.Helpers;

namespace FilmFlow.Domain.Entities
{
    public class CinemaShow : Entity
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Movie Movie { get; set; } = null!;

        public long MovieId { get; set; }

        public CinemaHall CinemaHall { get; set; } = null!;

        public long CinemaHallId { get; set; }

        public bool IsSecret { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

        public CinemaShow() { }

        public CinemaShow(DateTime start, DateTime end, Movie movie, CinemaHall hall, bool isSecret = false)
        {
            Start = start;
            End = end;
            Movie = movie;
            CinemaHall = hall;
            IsSecret = false;
        }
    }
}
