using FilmFlow.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.Server.Data.Configuration
{
    public class CinemaShowEntityTypeConfiguration
        : IEntityTypeConfiguration<CinemaShow>
    {
        public void Configure(EntityTypeBuilder<CinemaShow> builder)
        {
            builder.Property(cs => cs.Start)
                .IsRequired();

            builder.Property(cs => cs.End)
                .IsRequired();

            builder.Property(cs => cs.Price)
                .IsRequired();

            builder.HasOne(cs => cs.Movie)
                .WithMany(m => m.CinemaShows);

            builder.HasOne(cs => cs.CinemaHall)
                .WithMany();
        }
    }
}
