using Metallum.ETL.WorkerService;
using Metallum.ETL.WorkerService.Extract;
using Metallum.ETL.WorkerService.Load;
using Metallum.Infrastructure;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
  .ConfigureServices(services =>
  {
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddHostedService<Worker>();

    services.AddMetallumInfrastructure();

    services.AddTransient<BandExtractor>();
    services.AddTransient<BandLoader>();
  })
  .Build();

await host.RunAsync();
