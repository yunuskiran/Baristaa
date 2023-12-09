using System;
using System.Threading;
using System.Threading.Tasks;
using Baristaa.Models;
using Xunit;
using Moq;
using Baristaa.Core.Providers;

namespace Baristaa.Core.Handlers.Tests
{
    public class DateHandlerTests
    {
        private class MockRequestHandler : IRequestHandler
        {
            public IRequestHandler Next { get; }

            public ValueTask<Result> HandleAsync(CancellationToken cancellationToken = default)
            {
                return new ValueTask<Result>(new Result());
            }
        }

        [Fact]
        public async Task HandleAsync_April1_ReturnsTeapotResult()
        {
            // Arrange
            var mockRequestHandler = new MockRequestHandler();
            var datetimeProvider = new MockDateTimeProvider(new DateTime(2023, 4, 1));
            var dateHandler = new DateHandler(mockRequestHandler, datetimeProvider);

            // Act
            var result = await dateHandler.HandleAsync();

            // Assert
            Assert.Equal("I'm a teapot", result.Message);
            Assert.Equal(418, result.StatusCode);
        }

        [Fact]
        public async Task HandleAsync_NotApril1_CallsNextHandler()
        {
            // Arrange
            var mockRequestHandler = new MockRequestHandler();
            var datetimeProvider = new MockDateTimeProvider(new DateTime(2023, 5, 1));
            var dateHandler = new DateHandler(mockRequestHandler, datetimeProvider);

            // Act
            var result = await dateHandler.HandleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Same(mockRequestHandler, dateHandler.Next);
        }

        [Fact]
        public async Task HandleAsync_NoNextHandler_ReturnsResult()
        {
            // Arrange
            var datetimeProvider = new MockDateTimeProvider(new DateTime(2023, 5, 1));
            var dateHandler = new DateHandler(null, datetimeProvider);

            // Act
            var result = await dateHandler.HandleAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}
