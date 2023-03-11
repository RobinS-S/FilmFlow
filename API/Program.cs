using FilmFlow.API.Data;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Misc;
using FilmFlow.API.Services;
using FilmFlow.API.Swagger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FilmFlow
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<Config>(builder.Configuration)
                .AddScoped(sp => sp.GetRequiredService<IOptionsSnapshot<Config>>().Value);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version())));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();

            builder.Services.AddIdentityServer(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                }
            }).AddApiAuthorization<ApplicationUser, ApplicationDbContext>(o =>
            {
                if (builder.Environment.IsDevelopment()) o.Clients[0].RedirectUris.Add("/swagger/oauth2-redirect.html");
            });

            builder.Services.AddAuthentication()
                .AddIdentityServerJwt();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSwaggerGen();
            }

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<CinemaHallService>();
            builder.Services.AddScoped<CinemaHallRowService>();
            builder.Services.AddScoped<CinemaShowService>();
            builder.Services.AddScoped<MovieReviewService>();
            builder.Services.AddScoped<MovieService>();
            builder.Services.AddScoped<ReservationService>();
            builder.Services.AddScoped<ShowTicketService>();
            builder.Services.AddScoped<SocialService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            await DatabaseSeeder.SeedDatabase(app.Services); // Ensure the roles and a default user exists

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(setup =>
                {
                    setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1.0");
                    setup.OAuthClientId("FilmFlow.Client");
                    setup.OAuthAppName("FilmFlow API");
                    setup.OAuthScopeSeparator(" ");
                    setup.OAuthUsePkce();
                });
            }

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}