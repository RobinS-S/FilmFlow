using FilmFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
{
    public class MovieReviewEntityTypeConfiguration
        : IEntityTypeConfiguration<MovieReview>
    {
        public void Configure(EntityTypeBuilder<MovieReview> builder)
        {
            builder.Property(m => m.Stars)
                .IsRequired();

            builder.Property(mr => mr.MovieId)
                .IsRequired();

            builder.HasOne(mr => mr.Movie)
                .WithMany(m => m.MovieReviews)
                .HasForeignKey(mr => mr.MovieId);

            builder.HasOne(mr => mr.User)
                .WithMany()
                .HasForeignKey(mr => mr.UserId);

            builder.Property(mr => mr.UserId)
                .IsRequired();

            builder.Property(m => m.Text)
                .HasMaxLength(1024)
                .IsUnicode()
                .IsRequired();
        }
    }
}
