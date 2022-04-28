using AutoMapper;
using Metallum.Core;
using Metallum.Core.Models;

namespace Metallum.Web.Controllers
{
  internal class AggregateProfile : Profile
  {
    public AggregateProfile()
    {
      CreateMap<Aggregate, AggregateModel>()
        .ForMember(x => x.Id, x => x.MapFrom(y => y.Key));
    }
  }
}
