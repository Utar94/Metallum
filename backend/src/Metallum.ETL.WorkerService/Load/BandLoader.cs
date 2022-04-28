using Metallum.Core.Bands;
using Metallum.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Metallum.ETL.WorkerService.Load
{
  internal class BandLoader
  {
    private static readonly Guid userId = Guid.Empty;

    private readonly MetallumDbContext dbContext;
    private readonly ILogger<BandLoader> logger;

    public BandLoader(MetallumDbContext dbContext, ILogger<BandLoader> logger)
    {
      this.dbContext = dbContext;
      this.logger = logger;
    }

    public async Task ExecuteAsync(IEnumerable<Band> bands, CancellationToken cancellationToken)
    {
      ArgumentNullException.ThrowIfNull(bands);

      if (bands.Any())
      {
        int createCounter = 0;
        int updateCounter = 0;

        Dictionary<string, Band> existingBands = await dbContext.Bands
            .ToDictionaryAsync(x => x.MetallumId, x => x, cancellationToken);

        foreach (Band band in bands)
        {
          if (existingBands.TryGetValue(band.MetallumId, out Band? existingBand))
          {
            int modifications = 0;

            if (band.Genre != existingBand.Genre)
            {
              existingBand.Genre = band.Genre;
              modifications++;
            }
            if (band.Href != existingBand.Href)
            {
              existingBand.Href = band.Href;
              modifications++;
            }
            if (band.Location != existingBand.Location)
            {
              existingBand.Location = band.Location;
              modifications++;
            }
            if (band.Name != existingBand.Name)
            {
              existingBand.Name = band.Name;
              modifications++;
            }
            if (band.Status != existingBand.Status)
            {
              existingBand.Status = band.Status;
              modifications++;
            }

            if (modifications > 0)
            {
              updateCounter++;

              existingBand.Update(userId);
              await dbContext.SaveChangesAsync(cancellationToken);

              logger.LogInformation("Updated band: {name}", existingBand.Name);
            }
          }
          else
          {
            createCounter++;

            existingBand = new Band(userId)
            {
              Genre = band.Genre,
              Href = band.Href,
              Location = band.Location,
              MetallumId = band.MetallumId,
              Name = band.Name,
              Status = band.Status
            };

            dbContext.Bands.Add(existingBand);
            await dbContext.SaveChangesAsync(cancellationToken);

            existingBands.Add(existingBand.MetallumId, existingBand);

            logger.LogInformation("Created band: {name}", existingBand.Name);
          }
        }

        logger.LogInformation("Created {count} new bands.", createCounter);
        logger.LogInformation("Updated {count} existing bands.", updateCounter);
      }
      else
      {
        logger.LogWarning("No band to load.");
      }
    }
  }
}
