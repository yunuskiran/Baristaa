namespace Baristaa.Core.Services;

public class WeatherService : IWeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }


    public async ValueTask<double> GetTemp(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("weatherapi");

        var response = await client.GetAsync($"?lat=44.34&lon=10.99&appid={_configuration["WeatherApi:Key"]}&&units=metric", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return 0;
        }

        return (await response.Content.ReadFromJsonAsync<Temperatures>(cancellationToken: cancellationToken)).Main?.Temp ?? 0;
    }
}
