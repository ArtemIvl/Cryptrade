using CryptocurrencyData.Controllers;
using CryptocurrencyData.Entity;
using CryptocurrencyData.Interfaces;
using CryptocurrencyData.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests
{
    [TestFixture]
    public class CryptocurrencyServiceTests
    {
        private CryptoController _cryptoController;
        private Mock<ICryptoService> _cryptoServiceMock;

        [SetUp]
        public void Setup()
        {
            _cryptoServiceMock = new Mock<ICryptoService>();
            _cryptoController = new CryptoController(_cryptoServiceMock.Object);
        }

        [Test]
        public async Task GetCryptoData_SuccessfulRequest_ReturnsOkResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.GetCryptoDataAsync()).ReturnsAsync(new List<CryptoData>());

            // Act
            var result = await _cryptoController.GetCryptoData() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task GetCryptoData_ExceptionThrown_ReturnsInternalServerErrorResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.GetCryptoDataAsync()).Throws(new Exception("Some error"));

            // Act
            var result = await _cryptoController.GetCryptoData() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }


        [Test]
        public async Task GetNewCryptoData_SuccessfulRequest_ReturnsOkResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.GetCryptoDataFromApi()).ReturnsAsync(new List<CryptoData>());

            // Act
            var result = await _cryptoController.GetNewCryptoData() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task GetNewCryptoData_ExceptionThrown_ReturnsInternalServerErrorResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.GetCryptoDataFromApi()).Throws(new Exception("Some error"));

            // Act
            var result = await _cryptoController.GetNewCryptoData() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }


        [Test]
        public async Task SortByPriceChange_SuccessfulRequest_ReturnsOkResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.SortByPriceChangeDescending()).ReturnsAsync(new List<CryptoData>());

            // Act
            var result = await _cryptoController.SortByPriceChange() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task SortByVolume24h_SuccessfulRequest_ReturnsOkResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.SortByVolume24hDescending()).ReturnsAsync(new List<CryptoData>());

            // Act
            var result = await _cryptoController.SortByVolume24h() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task SearchCrypto_SuccessfulRequest_ReturnsOkResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.SearchCrypto(It.IsAny<string>())).ReturnsAsync(new List<CryptoData>());
            var searchTerm = "Bitcoin";

            // Act
            var result = await _cryptoController.SearchCrypto(searchTerm) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task SearchCrypto_ExceptionThrown_ReturnsInternalServerErrorResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.SearchCrypto(It.IsAny<string>())).Throws(new Exception("Some error"));
            var searchTerm = "Bitcoin";

            // Act
            var result = await _cryptoController.SearchCrypto(searchTerm) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async Task GetCryptoBySymbol_SuccessfulRequest_ReturnsOkResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.GetCryptoBySymbol(It.IsAny<string>())).ReturnsAsync(new CryptoData());
            var symbol = "BTC";

            // Act
            var result = await _cryptoController.GetCryptoBySymbol(symbol) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Value);
        }

        [Test]
        public async Task GetCryptoBySymbol_CryptoNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.GetCryptoBySymbol(It.IsAny<string>())).ReturnsAsync(null as CryptoData);
            var symbol = "BTC";

            // Act
            var result = await _cryptoController.GetCryptoBySymbol(symbol) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async Task GetCryptoBySymbol_ExceptionThrown_ReturnsInternalServerErrorResult()
        {
            // Arrange
            _cryptoServiceMock.Setup(x => x.GetCryptoBySymbol(It.IsAny<string>())).Throws(new Exception("Some error"));
            var symbol = "BTC";

            // Act
            var result = await _cryptoController.GetCryptoBySymbol(symbol) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }
    }
}
