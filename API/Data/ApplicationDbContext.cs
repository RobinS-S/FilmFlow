﻿using Duende.IdentityServer.EntityFramework.Options;
using FilmFlow.Domain.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace FilmFlow.API.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<CinemaHall> CinemaHalls { get; set; } = null!;

        public DbSet<CinemaHallRow> CinemaHallRows { get; set; } = null!;

        public DbSet<CinemaHallRowSeat> CinemaHallRowSeats { get; set; } = null!;

        public DbSet<CinemaShow> CinemaShows { get; set; } = null!;

        public DbSet<Movie> Movies { get; set; } = null!;

        public DbSet<Reservation> Reservations { get; set; } = null!;

        public DbSet<ShowTicket> ShowTickets { get; set; } = null!;

        public DbSet<MovieReview> MovieReviews { get; set; } = null!;

        public DbSet<Social> Socials { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}