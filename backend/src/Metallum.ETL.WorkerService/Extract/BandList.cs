using System.Text.Json.Serialization;

namespace Metallum.ETL.WorkerService.Extract
{
  internal class BandList
  {
    [JsonPropertyName("aaData")]
    public string[][] Data { get; set; } = null!;

    [JsonPropertyName("iTotalDisplayRecords")]
    public int TotalDisplayRecords { get; set; }

    [JsonPropertyName("iTotalRecords")]
    public int TotalRecords { get; set; }

    [JsonPropertyName("sEcho")]
    public int Echo { get; set; }
  }
}
