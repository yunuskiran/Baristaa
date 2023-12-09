namespace Baristaa.Core.Services;

public interface IWeatherService
{
    ValueTask<double> GetTemp(CancellationToken cancellationToken = default);
}
