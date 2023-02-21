using FilmFlow.API.Auth;
using FilmFlow.API.Data.Enums;
using FilmFlow.API.Data.Models;
using FilmFlow.API.Misc;
using Microsoft.AspNetCore.Identity;

namespace FilmFlow.API.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = scope.ServiceProvider.GetRequiredService<Config>();
            var adminRoleName = Roles.Admin;

            var roleExist = await roleManager.RoleExistsAsync(adminRoleName);
            if (!roleExist) await roleManager.CreateAsync(new IdentityRole(adminRoleName));

            if (string.IsNullOrWhiteSpace(configuration.AdminUserEmail) || string.IsNullOrWhiteSpace(configuration.AdminUserPassword))
                throw new Exception(
                    "You need to provide a default user account which will be created with the Admin role, keys: AppSettings:AdminUserEmail and AppSettings:AdminUserPassword");

            var defaultUser = new ApplicationUser
            {
                UserName = configuration.AdminUserEmail,
                Email = configuration.AdminUserEmail
            };

            var user = await userManager.FindByEmailAsync(configuration.AdminUserEmail);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(defaultUser, configuration.AdminUserPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin);
                    user = await userManager.FindByEmailAsync(configuration.AdminUserEmail);
                }
            }
            else
            {
                var isAdmin = await userManager.IsInRoleAsync(user, Roles.Admin);
                if (!isAdmin) await userManager.AddToRoleAsync(user, Roles.Admin);
            }

            if (user != null)
            {
                await SeedSampleData(dbContext, user);
            }
        }

        private static async Task SeedSampleData(ApplicationDbContext dbContext, ApplicationUser user)
        {
            if (dbContext.Movies.Any()) return;

            var halls = new List<CinemaHall>
            {
                new CinemaHall(10, 10, false, true),
                new CinemaHall(12, 12, true, true),
                new CinemaHall(8, 8, false, true)
            };

            var movies = new List<Movie>
            {
                new Movie("Toy Story 4", "Woody and the gang are back!", new DateTime(2019, 6, 20), "Animation", 0, "English"),
                new Movie("Joker", "The origin of the iconic villain", new DateTime(2019, 10, 4), "Drama", 16, "English"),
                new Movie("Avengers: Endgame", "The epic conclusion to the Infinity Saga", new DateTime(2019, 4, 26), "Action", 12, "English"),
                new Movie("Parasite", "A poor family becomes entangled with a wealthy one", new DateTime(2019, 11, 8), "Drama", 16, "Korean"),
                new Movie("The Lion King", "A remake of the classic Disney animated film", new DateTime(2019, 7, 19), "Animation", 0, "English")
            };

            var reviews = new List<MovieReview>
            {
                new MovieReview(movies[0], 4, user, "A great family movie"),
                new MovieReview(movies[1], 5, user, "Joaquin Phoenix gives a stunning performance"),
                new MovieReview(movies[2], 4, user, "A fitting conclusion to the Marvel Cinematic Universe"),
                new MovieReview(movies[3], 5, user, "A brilliantly crafted film with a powerful message"),
                new MovieReview(movies[4], 3, user, "A faithful adaptation, but lacks the charm of the original")
            };

            var shows = new List<CinemaShow>();
            foreach (var hall in halls)
            {
                foreach (var movie in movies)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var start = DateTime.Today.AddDays(i).AddHours(10);
                        var end = start.AddHours(2);
                        shows.Add(new CinemaShow(start, end, movie, hall));
                    }
                }
            }

            var reservations = new List<Reservation>
            {
                // Reservations for show 1 (cinema hall 1)
                new Reservation(shows[0], null, true, TarriffType.NORMAL, 1, 1),
                new Reservation(shows[0], null, false, TarriffType.CHILDREN, 2, 2),
    
                // Reservations for show 2 (cinema hall 2)
                new Reservation(shows[1], null, true, TarriffType.NORMAL, 3, 3),
                new Reservation(shows[1], null, false, TarriffType.CHILDREN, 4, 4),
    
                // Reservations for show 3 (cinema hall 3)
                new Reservation(shows[2], null, true, TarriffType.NORMAL, 5, 5),
                new Reservation(shows[2], null, false, TarriffType.CHILDREN, 6, 6),
    
                // Reservations for show 4 (cinema hall 1)
                new Reservation(shows[3], null, true, TarriffType.NORMAL, 7, 7),
                new Reservation(shows[3], null, false, TarriffType.CHILDREN, 8, 8),
    
                // Reservations for show 5 (cinema hall 2)
                new Reservation(shows[4], null, true, TarriffType.NORMAL, 9, 9),
                new Reservation(shows[4], null, false, TarriffType.CHILDREN, 10, 10),
    
                // Reservations for show 6 (cinema hall 3)
                new Reservation(shows[5], null, true, TarriffType.NORMAL, 11, 11),
                new Reservation(shows[5], null, false, TarriffType.CHILDREN, 12, 12),
    
                // Reservations for show 7 (cinema hall 1)
                new Reservation(shows[6], null, true, TarriffType.NORMAL, 13, 13),
                new Reservation(shows[6], null, false, TarriffType.CHILDREN, 14, 14),
    
                // Reservations for show 8 (cinema hall 2)
                new Reservation(shows[7], null, true, TarriffType.NORMAL, 15, 15),
                new Reservation(shows[7], null, false, TarriffType.CHILDREN, 16, 16),
    
                // Reservations for show 9 (cinema hall 3)
                new Reservation(shows[8], null, true, TarriffType.NORMAL, 17, 17),
                new Reservation(shows[8], null, false, TarriffType.CHILDREN, 18, 18),
    
                // Reservations for show 10 (cinema hall 1)
                new Reservation(shows[9], null, true, TarriffType.NORMAL, 19, 19),
                new Reservation(shows[9], null, false, TarriffType.CHILDREN, 20, 20)
            };

            var random = new Random();
            foreach (Reservation r in reservations)
            {
                r.Ticket = random.Next(100) > 70 ? new ShowTicket("Kiosk") : new ShowTicket("Kassiere Anna");
            }

            dbContext.CinemaHalls.AddRange(halls);
            dbContext.Movies.AddRange(movies);
            dbContext.MovieReviews.AddRange(reviews);
            dbContext.CinemaShows.AddRange(shows);
            dbContext.Reservations.AddRange(reservations);

            await dbContext.SaveChangesAsync();
        }
    }
}