using CryptocurrencyData.Controllers;
using CryptocurrencyData.Data;
using CryptocurrencyData.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests
{
    public class CryptoControllerIntegrationTests : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly CryptoController _cryptoController;
        private readonly CryptoService _cryptoService;

        public CryptoControllerIntegrationTests()
        {
            _httpClient = new HttpClient();

            _cryptoService = new CryptoService(new Mock<HttpClient>().Object, new Mock<IConfiguration>().Object, new Mock<CryptoDbContext>().Object, new Mock<RabbitMQService>().Object);

            _cryptoController = new CryptoController(_cryptoService);
        }

        [Fact]
        public async Task GetCryptoData_ReturnsOkResult()
        {
            // Act
            var result = await _cryptoController.GetCryptoData() as OkObjectResult;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetCryptoData_ExceptionThrown_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var mockCryptoService = new Mock<CryptoService>();
            mockCryptoService.Setup(x => x.GetCryptoDataAsync()).Throws(new Exception("Some error"));
            var cryptoControllerWithMockService = new CryptoController(mockCryptoService.Object);

            // Act
            var result = await cryptoControllerWithMockService.GetCryptoData() as ObjectResult;

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(500, result.StatusCode);
            Xunit.Assert.IsType<Exception>(result.Value);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
