using Imdb.Domain.Enteties;
using Imdb.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imdb.Persistence.EntityConfigurations
{
    public class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Value)
                .HasConversion(p => p.Value, p => RatingValue.Create(p).Value);
        }
    }
}
