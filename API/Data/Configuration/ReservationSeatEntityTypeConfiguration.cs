using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
{
    public class ReservationSeatEntityTypeConfiguration
        : IEntityTypeConfiguration<ReservationSeat>
    {
        public void Configure(EntityTypeBuilder<ReservationSeat> builder)
        {
            builder.Property(rs => rs.TariffType)
                .IsRequired();
        }
    }
}
