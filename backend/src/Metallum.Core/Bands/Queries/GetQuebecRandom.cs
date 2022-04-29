using MediatR;
using Metallum.Core.Bands.Models;

namespace Metallum.Core.Bands.Queries
{
  public class GetQuebecRandom : IRequest<IEnumerable<BandModel>>
  {
    public GetQuebecRandom(int count)
    {
      Count = count;
    }

    public int Count { get; }
  }
}
