using FilmFlow.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.Server.Data.Configuration
{
    public class MovieReviewEntityTypeConfiguration
        : IEntityTypeConfiguration<MovieReview>
    {
        public void Configure(EntityTypeBuilder<MovieReview> builder)
        {
            builder.HasOne(mr => mr.Movie)
                .WithMany(m => m.MovieReviews);

            builder.Property(m => m.Stars)
                .IsRequired();

            builder.Property(m => m.Author)
                .HasMaxLength(128)
                .IsUnicode()
                .IsRequired();

            builder.Property(m => m.Text)
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired();
        }
    }
}
