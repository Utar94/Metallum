using AutoMapper;
using MediatR;
using Metallum.Core.Bands.Models;
using Metallum.Core.Models;
using Metallum.Core.Repositories;

namespace Metallum.Core.Bands.Queries
{
  internal class GetBandsHandler : IRequestHandler<GetBands, ListModel<BandModel>>
  {
    private readonly IBandRepository bandRepository;
    private readonly IMapper mapper;

    public GetBandsHandler(IBandRepository bandRepository, IMapper mapper)
    {
      this.bandRepository = bandRepository;
      this.mapper = mapper;
    }

    public async Task<ListModel<BandModel>> Handle(GetBands request, CancellationToken cancellationToken)
    {
      PagedList<Band> bands = await bandRepository.GetPagedAsync(
        request.Deleted,
        request.Search,
        request.Status,
        request.Sort,
        request.Desc,
        request.Index,
        request.Count,
        readOnly: true,
        cancellationToken
      );

      return new ListModel<BandModel>(
        mapper.Map<IEnumerable<BandModel>>(bands),
        bands.Total
      );
    }
  }
}
