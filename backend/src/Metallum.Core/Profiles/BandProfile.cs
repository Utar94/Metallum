using AutoMapper;
using Metallum.Core.Bands;
using Metallum.Core.Bands.Models;
using Metallum.Core.Models;

namespace Metallum.Core.Profiles
{
  internal class BandProfile : Profile
  {
    public BandProfile()
    {
      CreateMap<Band, BandModel>()
        .IncludeBase<Aggregate, AggregateModel>();
    }
  }
}
