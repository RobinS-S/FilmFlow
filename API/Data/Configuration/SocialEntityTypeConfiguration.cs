using FilmFlow.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmFlow.API.Data.Configuration
{
    public class SocialEntityTypeConfiguration
        : IEntityTypeConfiguration<Social>
    {
        public void Configure(EntityTypeBuilder<Social> builder)
        {
            builder.Property(m => m.SocialName)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(m => m.Url)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(m => m.Icon)
				.HasMaxLength(255);
        }
	}
}
