using Metallum.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Metallum.Infrastructure.Configurations
{
  internal abstract class AggregateConfiguration
  {
    public virtual void Configure<T>(EntityTypeBuilder<T> builder) where T : Aggregate
    {
      builder.HasIndex(x => x.CreatedById);
      builder.HasIndex(x => x.Deleted);
      builder.HasIndex(x => x.DeletedById);
      builder.HasIndex(x => x.Key).IsUnique();
      builder.HasIndex(x => x.UpdatedById);

      builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
      builder.Property(x => x.Deleted).HasDefaultValue(false);
      builder.Property(x => x.Key).HasDefaultValueSql("uuid_generate_v4()");
      builder.Property(x => x.Version).HasDefaultValue(0);
    }
  }
}
