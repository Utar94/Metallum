using MediatR;
using Metallum.Core.Bands.Models;
using Metallum.Core.Models;

namespace Metallum.Core.Bands.Queries
{
  public class GetBands : IRequest<ListModel<BandModel>>
  {
    public bool? Deleted { get; set; }
    public string? Search { get; set; }
    public BandStatus? Status { get; set; }

    public BandSort? Sort { get; set; }
    public bool Desc { get; set; }

    public int? Index { get; set; }
    public int? Count { get; set; }
  }
}
