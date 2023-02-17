using FilmFlow.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.Server.Data.Configuration
{
    public class ReservationEntityTypeConfiguration
        : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(r => r.User)
                .WithMany();

            builder.HasOne(r => r.CinemaShow)
                .WithMany(cs => cs.Reservations);

            builder.Property(st => st.Code)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(r => r.SeatId)
                .IsRequired();

            builder.Property(r => r.RowId)
                .IsRequired();

            builder.Property(r => r.IsPaid)
                .IsRequired();

            builder.HasIndex(st => st.Code);
        }
    }
}
