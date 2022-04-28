using Logitar.WebApiToolKit;
using Metallum.Core;
using Metallum.Infrastructure;

namespace Metallum.Web
{
  public class Startup : StartupBase
  {
    private readonly ConfigurationOptions options = new();
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);

      services.AddWebApiToolKit(configuration, options);

      services.AddMetallumCore();
      services.AddMetallumInfrastructure();
    }

    public override void Configure(IApplicationBuilder applicationBuilder)
    {
      if (applicationBuilder is WebApplication application)
      {
        application.UseWebApiToolKit(options);
      }
    }
  }
}
