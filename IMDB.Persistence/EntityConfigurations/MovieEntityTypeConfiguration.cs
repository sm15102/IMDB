using Imdb.Domain.Enteties;
using Microsoft.EntityFrameworkCore;

namespace Imdb.Persistence.EntityConfigurations
{
    public class MovieEntityTypeConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CoverImageUrl).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(4000);
            builder.Property(x => x.ReleaseDate).IsRequired();

            builder.HasMany(x => x.Cast)
                .WithMany(x => x.Movies)
                .LeftNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Ratings)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade).
                 Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
