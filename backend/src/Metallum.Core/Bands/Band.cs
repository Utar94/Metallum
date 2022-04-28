namespace Metallum.Core.Bands
{
  public class Band : Aggregate
  {
    public Band(Guid userId) : base(userId)
    {
    }
    private Band() : base()
    {
    }

    public string Genre { get; set; } = null!;
    public string Href { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string MetallumId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public BandStatus Status { get; set; }
  }
}
