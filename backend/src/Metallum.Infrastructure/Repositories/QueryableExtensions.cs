using Microsoft.EntityFrameworkCore;

namespace Metallum.Infrastructure.Repositories
{
  internal static class QueryableExtensions
  {
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int? index, int? count)
    {
      ArgumentNullException.ThrowIfNull(query);

      if (index.HasValue)
      {
        query = query.Skip(index.Value);
      }
      if (count.HasValue)
      {
        query = query.Take(count.Value);
      }

      return query;
    }

    public static IQueryable<T> ApplyTracking<T>(this IQueryable<T> query, bool readOnly)
      where T : class
    {
      ArgumentNullException.ThrowIfNull(query);

      if (readOnly)
      {
        query = query.AsNoTracking();
      }

      return query;
    }
  }
}
