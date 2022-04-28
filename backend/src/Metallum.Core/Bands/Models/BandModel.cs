using Metallum.Core.Models;

namespace Metallum.Core.Bands.Models
{
  public class BandModel : AggregateModel
  {
    public string Genre { get; set; } = null!;
    public string Href { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string MetallumId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public BandStatus Status { get; set; }
  }
}
