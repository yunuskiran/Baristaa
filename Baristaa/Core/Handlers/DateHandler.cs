using Baristaa.Core.Providers;
using Baristaa.Models;

namespace Baristaa.Core.Handlers;

public class DateHandler : IRequestHandler
{
    public IRequestHandler Next { get; }

    private readonly IDateTimeProvider _datetimeProvider;

    public DateHandler(IRequestHandler next, IDateTimeProvider datetimeProvider)
    {
        Next = next;

        _datetimeProvider = datetimeProvider;
    }

    public ValueTask<Result> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = new Result();
        var currentDate = _datetimeProvider.GetCurrentDateTime();
        if (currentDate is { Month: 4, Day: 1 })
        {
            result.Message = "I'm a teapot";
            result.StatusCode = 418;
        }
        else if (Next != null)
        {
            return Next.HandleAsync(cancellationToken);
        }

        return new ValueTask<Result>(result);
    }
}