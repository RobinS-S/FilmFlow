using FilmFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
{
    public class ShowTicketEntityTypeConfiguration
        : IEntityTypeConfiguration<ShowTicket>
    {
        public void Configure(EntityTypeBuilder<ShowTicket> builder)
        {
            builder.Property(st => st.Code)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsRequired();

            builder.HasIndex(st => st.Code);

            builder.Property(st => st.SoldBy)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}
