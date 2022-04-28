using AutoMapper;
using Metallum.Core.Bands;
using Metallum.ETL.WorkerService.Extract;

namespace Metallum.ETL.WorkerService.Transform
{
  internal class BandProfile : Profile
  {
    public BandProfile()
    {
      CreateMap<BandData, Band>()
          .ForMember(x => x.Href, x => x.MapFrom(GetHref))
          .ForMember(x => x.MetallumId, x => x.MapFrom(GetMetallumId))
          .ForMember(x => x.Name, x => x.MapFrom(GetName))
          .ForMember(x => x.Status, x => x.MapFrom(GetStatus));
    }

    private static string GetHref(BandData data, Band band)
    {
      int startIndex = data.LinkHtml.IndexOf("'");
      int endIndex = data.LinkHtml.IndexOf("'", startIndex + 1);

      return data.LinkHtml[(startIndex + 1)..endIndex];
    }
    private static string GetMetallumId(BandData data, Band band)
    {
      string href = GetHref(data, band);

      return href[(href.LastIndexOf('/') + 1)..];
    }
    private static string GetName(BandData data, Band band)
    {
      int startIndex = data.LinkHtml.IndexOf('>');
      int endIndex = data.LinkHtml.LastIndexOf('<');

      return data.LinkHtml[(startIndex + 1)..endIndex];
    }
    private static BandStatus GetStatus(BandData data, Band band)
    {
      int startIndex = data.StatusHtml.IndexOf('>');
      int endIndex = data.StatusHtml.LastIndexOf('<');

      string statusText = data.StatusHtml[(startIndex + 1)..endIndex];

      return statusText switch
      {
        "Active" => BandStatus.Active,
        "Changed name" => BandStatus.ChangedName,
        "On hold" => BandStatus.OnHold,
        "Split-up" => BandStatus.SplitUp,
        "Unknown" => BandStatus.Unknown,
        _ => throw new ArgumentException($"The band status \"{statusText}\" is not valid.", nameof(data)),
      };
    }
  }
}
