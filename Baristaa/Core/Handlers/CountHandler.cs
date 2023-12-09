using Baristaa.Models;

namespace Baristaa.Core.Handlers;

public class CountHandler : IRequestHandler
{
    public IRequestHandler Next { get; }
    public int requestCount = 0;

    public CountHandler(IRequestHandler next)
    {
        Next = next;
    }

    public ValueTask<Result> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = new Result();

        requestCount++;
        if (requestCount % 5 == 0)
        {
            result.StatusCode = 503;
        }
        else if (Next != null)
        {
            return Next.HandleAsync(cancellationToken);
        }


        return new ValueTask<Result>(result);
    }
}