using Imdb.Domain.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Imdb.Persistence.EntityConfigurations
{
    public class ActorEntityTypeConfiguration : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Name, x =>
            {
                x.Property(p => p.First).IsRequired().HasColumnName("FirstName").HasMaxLength(50);
                x.Property(p => p.Last).IsRequired().HasColumnName("LastName").HasMaxLength(50);
            });
            builder.HasMany(x => x.Movies).WithMany(x => x.Cast)
                .LeftNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
