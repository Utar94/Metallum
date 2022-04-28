using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Metallum.Core
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddMetallumCore(this IServiceCollection services)
    {
      Assembly assembly = Assembly.GetExecutingAssembly();

      return services
        .AddAutoMapper(assembly)
        .AddMediatR(assembly);
    }
  }
}
