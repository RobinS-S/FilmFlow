using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
{
    public class ReservationEntityTypeConfiguration
        : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(r => r.Code)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsRequired();

            builder.HasIndex(r => r.Code);

            builder.HasOne(r => r.CinemaShow)
                .WithMany(st => st.Reservations);

            builder.Property(r => r.CinemaShowId)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany();

            builder.HasMany(r => r.ReservedSeats)
                .WithOne(rs => rs.Reservation);

            builder.Property(r => r.IsPaid)
                .IsRequired();
        }
    }
}
