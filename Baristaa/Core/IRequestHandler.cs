using Baristaa.Models;

namespace Baristaa.Core;

public interface IRequestHandler
{
    IRequestHandler Next { get; }
    ValueTask<Result> HandleAsync(CancellationToken cancellationToken = default);
}
