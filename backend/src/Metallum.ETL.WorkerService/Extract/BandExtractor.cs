using System.Text.Json;

namespace Metallum.ETL.WorkerService.Extract
{
  internal class BandExtractor : IDisposable
  {
    private const int PageSize = 500;
    private const string UrlFormat = "https://www.metal-archives.com/browse/ajax-country/c/CA/json/1?sEcho=1&iColumns=4&sColumns=&iDisplayStart={0}&iDisplayLength=500&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2&mDataProp_3=3&iSortCol_0=0&sSortDir_0=asc&iSortingCols=1&bSortable_0=true&bSortable_1=true&bSortable_2=true&bSortable_3=false&_=1651167951319";

    private readonly HttpClient client = new();
    private readonly ILogger<BandExtractor> logger;

    public BandExtractor(ILogger<BandExtractor> logger)
    {
      this.logger = logger;
    }

    public void Dispose()
    {
      client.Dispose();
    }

    public async Task<IEnumerable<BandData>> ExecuteAsync(CancellationToken cancellationToken)
    {
      List<BandData>? bands = null;

      int total = 0;
      int index = 0;
      do
      {
        var requestUri = new Uri(string.Format(UrlFormat, index));
        using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        using HttpResponseMessage response = await client.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
          string json = await response.Content.ReadAsStringAsync(cancellationToken);
          var bandList = JsonSerializer.Deserialize<BandList>(json);
          if (bandList != null)
          {
            bands ??= new List<BandData>(capacity: bandList.TotalRecords);
            total = bandList.TotalRecords;

            foreach (string[] data in bandList.Data)
            {
              if (data.Any())
              {
                bands.Add(new BandData(data));
              }
            }
          }

          logger.LogInformation("[{statusCode}] Retrieved {count} bands.", response.StatusCode, bandList?.Data.Length ?? 0);
        }
        else
        {
          string? json = null;
          try
          {
            json = await response.Content.ReadAsStringAsync(cancellationToken);
          }
          catch (Exception)
          {
          }

          logger.LogError("[{statusCode}] {reasonPhrase} | {json}", response.StatusCode, response.ReasonPhrase, json);
        }

        index += PageSize;
      } while (index <= total);

      return bands ?? Enumerable.Empty<BandData>();
    }
  }
}
