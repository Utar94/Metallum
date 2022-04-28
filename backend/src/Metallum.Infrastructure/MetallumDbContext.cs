#nullable disable
using Metallum.Core.Bands;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Metallum.Infrastructure
{
  public class MetallumDbContext : DbContext
  {
    public MetallumDbContext(DbContextOptions<MetallumDbContext> options) : base(options)
    {
    }

    public DbSet<Band> Bands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
