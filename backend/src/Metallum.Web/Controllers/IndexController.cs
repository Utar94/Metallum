using Logitar.WebApiToolKit.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Metallum.Web.Controllers
{
  [ApiExplorerSettings(IgnoreApi = true)]
  [Route("")]
  public class IndexController : ControllerBase
  {
    private readonly ApiSettings apiSettings;

    public IndexController(ApiSettings apiSettings)
    {
      this.apiSettings = apiSettings;
    }

    public IActionResult Get() => Ok(apiSettings.Name);
  }
}
