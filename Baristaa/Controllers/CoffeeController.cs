using Baristaa.Core;
using Baristaa.Core.Handlers;
using Baristaa.Core.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Baristaa.Controllers;

[ApiController]
public class CoffeeController : ControllerBase
{
    private readonly IRequestHandler _requestHandler;

    public CoffeeController(IRequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    [HttpGet("brew-coffee")]
    public async Task<IActionResult> BrewCoffee()
    {
        var result = await _requestHandler.HandleAsync();

        return StatusCode(result.StatusCode,
            result.StatusCode != 200
                ? Empty
                : new
                {
                    result.Message,
                    result.Prepared
                });
    }
}