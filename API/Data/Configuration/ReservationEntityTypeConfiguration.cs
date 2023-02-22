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

            builder.HasAlternateKey(r => new { r.CinemaShowId, r.RowId, r.SeatId });

            builder.HasOne(r => r.CinemaShow)
                .WithMany(st => st.Reservations);

            builder.Property(r => r.CinemaShowId)
                .IsRequired();

            builder.HasOne(r => r.Row)
                .WithMany()
                .HasForeignKey(r => r.RowId);

            builder.Property(r => r.RowId)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany();

            builder.HasOne(r => r.Ticket)
                .WithOne(st => st.Reservation)
                .HasForeignKey<Reservation>(r => r.TicketId);

            builder.Property(r => r.IsPaid)
                .IsRequired();

            builder.Property(r => r.TarriffType)
                .IsRequired();

            builder.Property(r => r.SeatId)
                .IsRequired();
        }
    }
}
