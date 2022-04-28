namespace Metallum.ETL.WorkerService.Extract
{
  internal class BandData
  {
    public BandData()
    {
    }
    public BandData(string[] data)
    {
      ArgumentNullException.ThrowIfNull(data);

      if (data.Length < 4)
      {
        throw new ArgumentException("At least 4 values are required.", nameof(data));
      }

      LinkHtml = data[0];
      Genre = data[1];
      Location = data[2];
      StatusHtml = data[3];
    }

    public string Genre { get; set; } = null!;
    public string LinkHtml { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string StatusHtml { get; set; } = null!;
  }
}
