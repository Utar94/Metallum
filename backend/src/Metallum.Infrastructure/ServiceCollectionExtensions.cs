using Metallum.Core.Repositories;
using Metallum.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metallum.Infrastructure
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddMetallumInfrastructure(this IServiceCollection services)
    {
      return services
        .AddDbContext<MetallumDbContext>((provider, builder) =>
        {
          var configuration = provider.GetRequiredService<IConfiguration>();
          builder.UseNpgsql(configuration.GetConnectionString(nameof(MetallumDbContext)));
        })
        .AddScoped<IBandRepository, BandRepository>();
    }
  }
}
