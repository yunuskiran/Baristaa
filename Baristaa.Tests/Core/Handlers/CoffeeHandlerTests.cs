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
            var nextHandlerMock = new Mock<IRequestHandler>();
            var coffeeHandler = new CoffeeHandler(nextHandlerMock.Object);

            // Act
            await coffeeHandler.HandleAsync();

            // Assert
            nextHandlerMock.Verify(x => x.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WithoutNextHandler_ReturnsResult()
        {
            // Arrange
            var coffeeHandler = new CoffeeHandler(null);

            // Act
            var result = await coffeeHandler.HandleAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}
