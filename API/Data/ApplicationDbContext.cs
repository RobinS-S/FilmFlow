using Duende.IdentityServer.EntityFramework.Options;
using FilmFlow.Server.Data.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FilmFlow.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<CinemaHall> CinemaHalls { get; set; } = null!;

        public DbSet<CinemaShow> CinemaShows { get; set; } = null!;

        public DbSet<Movie> Movies { get; set; } = null!;

        public DbSet<Reservation> Reservations { get; set; } = null!;

        public DbSet<ShowTicket> ShowTickets { get; set; } = null!;

        public DbSet<MovieReview> MovieReviews { get; set; } = null!;
    }
}