using FilmFlow.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.Server.Data.Configuration
{
    public class MovieEntityTypeConfiguration
        : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(m => m.Title)
                .HasMaxLength(128)
                .IsUnicode()
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired();

            builder.Property(m => m.Category)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();
        }
    }
}
