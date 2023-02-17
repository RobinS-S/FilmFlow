﻿namespace FilmFlow.Server.Data.Models
{
    public class CinemaShow : Entity
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public decimal Price { get; set; }

        public Movie Movie { get; set; } = null!;

        public CinemaHall CinemaHall { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

        public ICollection<ShowTicket> ShowTickets { get; set; } = new HashSet<ShowTicket>();

        public CinemaShow() { }

        public CinemaShow(DateTime start, DateTime end, decimal price, Movie movie, CinemaHall hall) 
        {
            Start = start;
            End = end;
            Price = price;
            Movie = movie;
            CinemaHall = hall;
        }
    }
}
