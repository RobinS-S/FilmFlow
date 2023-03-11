using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
{
    public class CinemaHallRowSeatEntityTypeConfiguration
    : IEntityTypeConfiguration<CinemaHallRowSeat>
    {
        public void Configure(EntityTypeBuilder<CinemaHallRowSeat> builder)
        {
            builder.Property(chrs => chrs.SeatNumber)
                .IsRequired();
        }
    }
}
