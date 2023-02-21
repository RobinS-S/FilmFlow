using FilmFlow.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
{
    public class CinemaHallRowEntityTypeConfiguration
    : IEntityTypeConfiguration<CinemaHallRow>
    {
        public void Configure(EntityTypeBuilder<CinemaHallRow> builder)
        {
            builder.HasAlternateKey(chr => new { chr.HallId, chr.RowId });

            builder.Property(chr => chr.HallId)
                .IsRequired();

            builder.HasOne(chr => chr.Hall)
                .WithMany(ch => ch.Rows);
        }
    }
}
