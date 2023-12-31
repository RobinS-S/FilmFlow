﻿using FilmFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
{
    public class CinemaHallEntityTypeConfiguration
        : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            builder.Property(ch => ch.IsThreeDimensional)
                .IsRequired();

            builder.Property(ch => ch.IsWheelchairFriendly)
                .IsRequired();
        }
    }
}
