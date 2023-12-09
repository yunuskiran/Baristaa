using Baristaa.Core.Services;
using Baristaa.Models;

namespace Baristaa.Core.Handlers;

public class CoffeeHandler : IRequestHandler
{
    public IRequestHandler Next { get; }

    private readonly IWeatherService _weatherService;

    public CoffeeHandler(IWeatherService weatherService, IRequestHandler next)
    {
        Next = next;

        _weatherService = weatherService;
    }

    public async ValueTask<Result> HandleAsync(CancellationToken cancellationToken = default)
    {
        var temp = await _weatherService.GetTemp(cancellationToken);
        var result = new Result();
        if (temp >= 30)
        {
            result.Message = "Your refreshing iced coffee is ready";
        }

        if (Next != null)
        {
            return await Next.HandleAsync(cancellationToken);
        }

        return result;
    }
}
