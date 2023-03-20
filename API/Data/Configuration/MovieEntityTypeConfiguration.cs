using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
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

            builder.Property(m => m.ReleaseDate)
                .IsRequired();

            builder.Property(m => m.Category)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

            builder.Property(m => m.MinAge)
                .IsRequired();

            builder.Property(m => m.Language)
                .HasMaxLength(64)
                .IsUnicode()
                .IsRequired();

            builder.Property(m => m.ImageUrl)
                .HasMaxLength(512)
                .IsUnicode()
                .IsRequired();
        }
    }
}
