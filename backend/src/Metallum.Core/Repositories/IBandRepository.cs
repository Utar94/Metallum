using Metallum.Core.Bands;

namespace Metallum.Core.Repositories
{
  public interface IBandRepository
  {
    Task<PagedList<Band>> GetPagedAsync(
      bool? deleted = null,
      string? search = null,
      BandStatus? status = null,
      BandSort? sort = null,
      bool desc = false,
      int? index = null,
      int? count = null,
      bool readOnly = false,
      CancellationToken cancellationToken = default
    );
  }
}
