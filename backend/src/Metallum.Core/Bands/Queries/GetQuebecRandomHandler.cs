using AutoMapper;
using MediatR;
using Metallum.Core.Bands.Models;
using Metallum.Core.Repositories;

namespace Metallum.Core.Bands.Queries
{
  internal class GetQuebecRandomHandler : IRequestHandler<GetQuebecRandom, IEnumerable<BandModel>>
  {
    private readonly IBandRepository bandRepository;
    private readonly IMapper mapper;

    public GetQuebecRandomHandler(IBandRepository bandRepository, IMapper mapper)
    {
      this.bandRepository = bandRepository;
      this.mapper = mapper;
    }

    public async Task<IEnumerable<BandModel>> Handle(GetQuebecRandom request, CancellationToken cancellationToken)
    {
      IEnumerable<Band> bands = await bandRepository.GetQuebecRandomAsync(request.Count, cancellationToken);

      return mapper.Map<IEnumerable<BandModel>>(bands);
    }
  }
}
