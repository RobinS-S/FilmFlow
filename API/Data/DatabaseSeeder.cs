using FilmFlow.API.Auth;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Misc;
using FilmFlow.Shared.Enums;
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
            var adminRoleName = Roles.AdminRoleName;

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
                    await userManager.AddToRoleAsync(defaultUser, Roles.AdminRoleName);
                    user = await userManager.FindByEmailAsync(configuration.AdminUserEmail);
                }
            }
            else
            {
                var isAdmin = await userManager.IsInRoleAsync(user, Roles.AdminRoleName);
                if (!isAdmin) await userManager.AddToRoleAsync(user, Roles.AdminRoleName);
            }

            if (user != null)
            {
                await SeedSampleData(dbContext, user);
            }
        }

        private static async Task SeedSampleData(ApplicationDbContext dbContext, ApplicationUser user)
        {
            if (dbContext.Movies.Any()) return; // If the database has been touched, don't seed any more data

            var halls = new List<CinemaHall>();
            halls.AddRange(Enumerable.Range(1, 2).Select(_ => new CinemaHall(
                    Enumerable.Range(1, 8).Select(i => new CinemaHallRow(15, i)).ToList(),
                    true, true)));

            halls.Add(new CinemaHall(
                    Enumerable.Range(1, 8).Select(i => new CinemaHallRow(15, i)).ToList(),
                    false, true));

            halls.Add(new CinemaHall(Enumerable.Range(1, 6).Select(i => new CinemaHallRow(10, i)).ToList(),
                    false, true));

            halls.AddRange(Enumerable.Range(1, 2).Select(_ => new CinemaHall(
                    Enumerable.Range(1, 2).Select(i => new CinemaHallRow(10, i)).Concat(
                        Enumerable.Range(3, 4).Select(i => new CinemaHallRow(15, i))).ToList(),
                    false, false)));

            var movies = new List<Movie>
            {
                new("Toy Story 4", "Woody and the gang are back!", new DateTime(2019, 6, 20), "Animation", 0, "English", "https://m.media-amazon.com/images/M/MV5BMTYzMDM4NzkxOV5BMl5BanBnXkFtZTgwNzM1Mzg2NzM@._V1_.jpg"),
                new("Joker", "The origin of the iconic villain", new DateTime(2019, 10, 4), "Drama", 16, "English","https://m.media-amazon.com/images/M/MV5BNGVjNWI4ZGUtNzE0MS00YTJmLWE0ZDctN2ZiYTk2YmI3NTYyXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_.jpg"),
                new("Avengers: Endgame", "The epic conclusion to the Infinity Saga", new DateTime(2019, 4, 26), "Action", 12, "English", "https://cdn.marvel.com/content/1x/avengersendgame_lob_crd_05.jpg"),
                new("Parasite", "A poor family becomes entangled with a wealthy one", new DateTime(2019, 11, 8), "Drama", 16, "Korean", "https://images.pathe-thuis.nl/20648_450x640.jpg"),
                new("The Lion King", "A remake of the classic Disney animated film", new DateTime(2019, 7, 19), "Animation", 0, "English","https://images.pathe-thuis.nl/19492_450x640.jpg")
            };

            var reviews = new List<MovieReview>
            {
                new(movies[0], 4, user, "A great family movie"),
                new(movies[1], 5, user, "Joaquin Phoenix gives a stunning performance"),
                new(movies[2], 4, user, "A fitting conclusion to the Marvel Cinematic Universe"),
                new(movies[3], 5, user, "A brilliantly crafted film with a powerful message"),
                new(movies[4], 3, user, "A faithful adaptation, but lacks the charm of the original")
            };

            var shows = new List<CinemaShow>();
            var random = new Random();
            foreach (var hall in halls)
            {
                foreach (var movie in movies)
                {
                    for (var i = 1; i < 7; i++)
                    {
                        var start = DateTime.Today.AddDays(i).AddHours(2);
                        var end = start.AddHours(2);
                        shows.Add(new CinemaShow(start, end, movie, hall));
                    }
                }
            }
            shows.First().IsSecret = true;
            shows.ElementAt(random.Next(0, shows.Count - 1)).IsSecret = true;

            var show = shows.ElementAt(0);
            var reservations = new List<Reservation>
            {
                new(show, new List<ReservationSeat>() { new(show.CinemaHall.Rows.ElementAt(1).Seats.ElementAt(3), TariffType.Normal) }, false, user)
            };

            var socials = new List<Social>
            {
                new("Instagram", "https://www.instagram.com/avanshogeschool/", "https://cdn-icons-png.flaticon.com/512/174/174855.png"),
                new("Facebook", "https://www.facebook.com/avans/?locale=nl_NL", "https://cdn-icons-png.flaticon.com/512/733/733547.png"),
                new("Linkedin", "https://nl.linkedin.com/school/avans-hogeschool/", "https://cdn-icons-png.flaticon.com/512/3536/3536505.png")
            };

            dbContext.CinemaHalls.AddRange(halls);
            dbContext.Movies.AddRange(movies);
            dbContext.MovieReviews.AddRange(reviews);
            dbContext.CinemaShows.AddRange(shows);
            dbContext.Reservations.AddRange(reservations);
            dbContext.Socials.AddRange(socials);

            await dbContext.SaveChangesAsync();
        }
    }
}