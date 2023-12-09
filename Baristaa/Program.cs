using Baristaa.Core;
using Baristaa.Core.Handlers;
using Baristaa.Core.Providers;
using Baristaa.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient("weatherapi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WeatherApi:Address"]!);
});

builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddTransient<IRequestHandler>(x =>
        new CoffeeHandler(x.GetRequiredService<IWeatherService>(), x.GetRequiredService<DateHandler>())
    );

builder.Services.AddScoped(x =>
    new DateHandler(x.GetRequiredService<CountHandler>(), x.GetRequiredService<IDateTimeProvider>())
);

builder.Services.AddSingleton(x =>
    new CountHandler(null)
);

builder.Services.AddScoped<IEnumerable<IRequestHandler>>(x =>
    x.GetServices<IRequestHandler>()
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();