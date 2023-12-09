using Baristaa.Core.Services;
using Baristaa.Models;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Baristaa.Core.Handlers.Tests
{
    public class CoffeHandlerTests
    {
        [Fact]
        public async Task HandleAsync_WithNextHandler_CallsNextHandler()
        {
            // Arrange
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(service => service.GetTemp(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(31);

            var nextHandlerMock = new Mock<IRequestHandler>();
            var coffeeHandler = new CoffeeHandler(weatherServiceMock.Object, nextHandlerMock.Object);

            // Act
            await coffeeHandler.HandleAsync();

            // Assert
            nextHandlerMock.Verify(x => x.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithoutNextHandler_ReturnsResult()
        {
            // Arrange
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(service => service.GetTemp(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(31);
            var nextHandlerMock = new Mock<IRequestHandler>();
            var coffeeHandler = new CoffeeHandler(weatherServiceMock.Object, null);

            // Act
            var result = await coffeeHandler.HandleAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task HandleAsync_TemperatureAbove30_ReturnsMessage()
        {
            // Arrange
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(service => service.GetTemp(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(31);

            var nextHandlerMock = new Mock<IRequestHandler>();
            var coffeeHandler = new CoffeeHandler(weatherServiceMock.Object, null);

            // Act
            var result = await coffeeHandler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Your refreshing iced coffee is ready", result.Message);
        }

        [Fact]
        public async Task HandleAsync_TemperatureBelow30_ReturnsEmptyMessage()
        {
            // Arrange
            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(service => service.GetTemp(It.IsAny<CancellationToken>()))
                             .ReturnsAsync(25);

            var nextHandlerMock = new Mock<IRequestHandler>();
            var coffeeHandler = new CoffeeHandler(weatherServiceMock.Object, null);

            // Act
            var result = await coffeeHandler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Your piping hot coffee is ready", result.Message);
        }
    }
}
