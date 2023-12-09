using Baristaa.Models;

namespace Baristaa.Core.Handlers;

public class CoffeeHandler : IRequestHandler
{
    public IRequestHandler Next { get; }


    public CoffeeHandler(IRequestHandler next)
    {
        Next = next;

    }

    public async ValueTask<Result> HandleAsync(CancellationToken cancellationToken = default)
    {
        if (Next != null)
        {
            return await Next.HandleAsync(cancellationToken);
        }

        return new Result();
    }
}
