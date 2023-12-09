using System.Threading;
using System.Threading.Tasks;
using Baristaa.Models;
using Moq;
using Xunit;

namespace Baristaa.Core.Handlers.Tests
{
    public class CountHandlerTests
    {
        [Fact]
        public async Task HandleAsync_RequestCountNotMultipleOfFive_ReturnsResultWithStatusCodeZero()
        {
            // Arrange
            var countHandler = new CountHandler(null);

            // Act
            var result = await countHandler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task HandleAsync_RequestCountIsMultipleOfFive_ReturnsResultWithStatusCode503()
        {
            // Arrange
            var nextHandlerMock = new Mock<IRequestHandler>();
            var countHandler = new CountHandler(nextHandlerMock.Object);
            countHandler.requestCount = 0;
            // Act

            for (int i = 0; i < 4; i++)
            {
                await countHandler.HandleAsync();
            }

            var result = await countHandler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(503, result.StatusCode);
        }
    }
}
