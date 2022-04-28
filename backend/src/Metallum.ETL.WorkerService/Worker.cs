using AutoMapper;
using Metallum.Core.Bands;
using Metallum.ETL.WorkerService.Extract;
using Metallum.ETL.WorkerService.Load;

namespace Metallum.ETL.WorkerService
{
  internal class Worker : BackgroundService
  {
    private readonly ILogger<Worker> logger;
    private readonly IMapper mapper;
    private readonly IServiceProvider serviceProvider;

    public Worker(ILogger<Worker> logger, IMapper mapper, IServiceProvider serviceProvider)
    {
      this.logger = logger;
      this.mapper = mapper;
      this.serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        using IServiceScope scope = serviceProvider.CreateScope();

        logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        var extractor = scope.ServiceProvider.GetRequiredService<BandExtractor>();
        IEnumerable<BandData> data = await extractor.ExecuteAsync(cancellationToken);

        if (data.Any())
        {
          var bands = mapper.Map<IEnumerable<Band>>(data);

          var loader = scope.ServiceProvider.GetRequiredService<BandLoader>();
          await loader.ExecuteAsync(bands, cancellationToken);
        }

        await Task.Delay(60000, cancellationToken);
      }
    }
  }
}
