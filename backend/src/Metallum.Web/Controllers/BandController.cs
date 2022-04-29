using MediatR;
using Metallum.Core.Bands;
using Metallum.Core.Bands.Models;
using Metallum.Core.Bands.Queries;
using Metallum.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Metallum.Web.Controllers
{
  [ApiController]
  [Route("bands")]
  public class BandController : ControllerBase
  {
    private readonly IMediator mediator;

    public BandController(IMediator mediator)
    {
      this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ListModel<BandModel>>> GetAsync(
      bool? deleted,
      string? search,
      BandStatus? status,
      BandSort? sort,
      bool desc,
      int? index,
      int? count,
      CancellationToken cancellationToken
    )
    {
      return Ok(await mediator.Send(new GetBands
      {
        Deleted = deleted,
        Search = search,
        Status = status,
        Sort = sort,
        Desc = desc,
        Index = index,
        Count = count
      }, cancellationToken));
    }

    [HttpGet("quebec/random")]
    public async Task<ActionResult<IEnumerable<BandModel>>> GetQuebecRandomAsync(
      int count = 10,
      CancellationToken cancellationToken = default
    )
    {
      return Ok(await mediator.Send(new GetQuebecRandom(count), cancellationToken));
    }
  }
}
