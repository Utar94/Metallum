using Metallum.Core.Bands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metallum.Infrastructure.Configurations
{
  internal class BandConfiguration : AggregateConfiguration, IEntityTypeConfiguration<Band>
  {
    public void Configure(EntityTypeBuilder<Band> builder)
    {
      base.Configure(builder);

      builder.HasIndex(x => x.Genre);
      builder.HasIndex(x => x.Location);
      builder.HasIndex(x => x.MetallumId).IsUnique();
      builder.HasIndex(x => x.Name);
      builder.HasIndex(x => x.Status);

      builder.Property(x => x.Genre).HasMaxLength(100);
      builder.Property(x => x.Href).HasMaxLength(2048);
      builder.Property(x => x.Location).HasMaxLength(200);
      builder.Property(x => x.MetallumId).HasMaxLength(16);
      builder.Property(x => x.Name).HasMaxLength(100);
    }
  }
}
