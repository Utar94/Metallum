using Metallum.Core;
using Metallum.Core.Bands;
using Metallum.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Metallum.Infrastructure.Repositories
{
  internal class BandRepository : RepositoryBase<Band>, IBandRepository
  {
    public BandRepository(MetallumDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedList<Band>> GetPagedAsync(
      bool? deleted = null,
      string? search = null,
      BandStatus? status = null,
      BandSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    )
    {
      IQueryable<Band> query = DbContext.Bands.ApplyTracking(readOnly);

      if (deleted.HasValue)
      {
        query = query.Where(x => x.Deleted == deleted.Value);
      }
      if (search != null)
      {
        query = query.Where(x => x.Genre.Contains(search)
          || x.Location.Contains(search)
          || x.Name.Contains(search));
      }
      if (status.HasValue)
      {
        query = query.Where(x => x.Status == status.Value);
      }

      long total = await query.LongCountAsync(cancellationToken);

      if (sort.HasValue)
      {
        query = sort.Value switch
        {
          BandSort.Genre => desc ? query.OrderByDescending(x => x.Genre) : query.OrderBy(x => x.Genre),
          BandSort.Location => desc ? query.OrderByDescending(x => x.Location) : query.OrderBy(x => x.Location),
          BandSort.Name => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
          BandSort.Status => desc ? query.OrderByDescending(x => x.Status) : query.OrderBy(x => x.Status),
          BandSort.UpdatedAt => desc ? query.OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt) : query.OrderBy(x => x.UpdatedAt ?? x.CreatedAt),
          _ => throw new ArgumentException($"The band sort \"{sort}\" is not valid.", nameof(sort)),
        };
      }

      query = query.ApplyPaging(index, count);

      Band[] bands = await query.ToArrayAsync(cancellationToken);

      return new PagedList<Band>(bands, total);
    }

    public async Task<IEnumerable<Band>> GetQuebecRandomAsync(int count, CancellationToken cancellationToken = default)
    {
      if (count < 1 || count > 1000)
      {
        throw new ArgumentException("The value must be between 1 and 1000.", nameof(count));
      }

      return await DbContext.Bands
        .FromSqlInterpolated($@"
SELECT *
FROM ""Bands""
WHERE ""Deleted"" = false
  AND lower(unaccent(""Location"")) LIKE '%quebec%'
  AND ""Status"" = {(int)BandStatus.Active}
ORDER BY random()
LIMIT {count};")
        .ToArrayAsync(cancellationToken);
    }
  }
}
