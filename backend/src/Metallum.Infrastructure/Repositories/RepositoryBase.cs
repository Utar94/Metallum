using Metallum.Core;

namespace Metallum.Infrastructure.Repositories
{
  internal class RepositoryBase<T> where T : Aggregate
  {
    protected RepositoryBase(MetallumDbContext dbContext)
    {
      DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    protected MetallumDbContext DbContext { get; }
  }
}
