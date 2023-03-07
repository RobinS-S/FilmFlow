using FilmFlow.API.Auth;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Data.Enums;
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
            if (dbContext.Movies.Any()) return; // If the database has been touched, don't seed any more data

            var halls = new List<CinemaHall>
            {
                new CinemaHall(Enumerable.Range(1, 5).Select(i => new CinemaHallRow(8, i)).ToList(), false, false),
                new CinemaHall(Enumerable.Range(1, 8).Select(i => new CinemaHallRow(10, i)).ToList(), false, true),
                new CinemaHall(Enumerable.Range(1, 12).Select(i => new CinemaHallRow(12, i)).ToList(), true, true),
            };

            var movies = new List<Movie>
            {
                new Movie("Toy Story 4", "Woody and the gang are back!", new DateTime(2019, 6, 20), "Animation", 0, "English", "https://m.media-amazon.com/images/M/MV5BMTYzMDM4NzkxOV5BMl5BanBnXkFtZTgwNzM1Mzg2NzM@._V1_.jpg"),
                new Movie("Joker", "The origin of the iconic villain", new DateTime(2019, 10, 4), "Drama", 16, "English","https://m.media-amazon.com/images/M/MV5BNGVjNWI4ZGUtNzE0MS00YTJmLWE0ZDctN2ZiYTk2YmI3NTYyXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_.jpg"),
                new Movie("Avengers: Endgame", "The epic conclusion to the Infinity Saga", new DateTime(2019, 4, 26), "Action", 12, "English", "https://cdn.marvel.com/content/1x/avengersendgame_lob_crd_05.jpg"),
                new Movie("Parasite", "A poor family becomes entangled with a wealthy one", new DateTime(2019, 11, 8), "Drama", 16, "Korean", "https://images.pathe-thuis.nl/20648_450x640.jpg"),
                new Movie("The Lion King", "A remake of the classic Disney animated film", new DateTime(2019, 7, 19), "Animation", 0, "English","https://images.pathe-thuis.nl/19492_450x640.jpg")
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

            var show = shows.ElementAt(0);
            var reservations = new List<Reservation>
            {
                new Reservation(show, new List<ReservationSeat>() { new ReservationSeat(new CinemaHallRowSeat(3, show.CinemaHall.Rows.ElementAt(1)), TarriffType.NORMAL) }, false, user)
            };

            dbContext.CinemaHalls.AddRange(halls);
            dbContext.Movies.AddRange(movies);
            dbContext.MovieReviews.AddRange(reviews);
            dbContext.CinemaShows.AddRange(shows);
            dbContext.Reservations.AddRange(reservations);

            await dbContext.SaveChangesAsync();
        }
    }
}