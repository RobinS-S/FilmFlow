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
            builder.Property(r => r.Code)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsRequired();

            builder.HasIndex(st => st.Code);

            builder.HasOne(r => r.CinemaShow)
                .WithMany(st => st.Reservations)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany();

            builder.HasOne(r => r.Ticket)
                .WithOne(st => st.Reservation)
                .HasForeignKey<Reservation>(r => r.TicketId);

            builder.HasOne(r => r.User)
                .WithMany()
                .IsRequired();

            builder.Property(r => r.IsPaid)
                .IsRequired();

            builder.Property(r => r.TarriffType)
                .IsRequired();

            builder.Property(r => r.SeatId)
                .IsRequired();

            builder.Property(r => r.RowId)
                .IsRequired();
        }
    }
}
