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

            builder.HasIndex(st => st.Code);

            builder.Property(st => st.SoldBy)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}
