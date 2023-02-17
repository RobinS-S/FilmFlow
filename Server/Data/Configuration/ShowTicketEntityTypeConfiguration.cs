using FilmFlow.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.Server.Data.Configuration
{
    public class ShowTicketEntityTypeConfiguration
        : IEntityTypeConfiguration<ShowTicket>
    {
        public void Configure(EntityTypeBuilder<ShowTicket> builder)
        {
            builder.Property(st => st.Code)
                .HasMaxLength(32)
                .IsUnicode(false)
                .IsRequired();

            builder.HasOne(st => st.CinemaShow)
                .WithMany(cs => cs.ShowTickets);

            builder.HasIndex(st => st.Code);
        }
    }
}
