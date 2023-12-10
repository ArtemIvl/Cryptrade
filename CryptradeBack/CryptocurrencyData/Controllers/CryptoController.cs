using System;
using CryptocurrencyData.Entity;
using CryptocurrencyData.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
	{
        private readonly CryptoService _cryptoService;

        public CryptoController(CryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCryptoData()
        {
            try
            {
                var cryptoData = await _cryptoService.GetCryptoDataAsync();
                return Ok(cryptoData);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, new { message = "An error occurred while fetching data." + ex.ToString() });
            }
        }

        [HttpGet("get-sorted-by-price-data")]
        public async Task<IActionResult> SortByPriceChange()
        {
            var sortedData = await _cryptoService.SortByPriceChangeDescending();
            return Ok(sortedData);
        }

        [HttpGet("get-sorted-by-volume-data")]
        public async Task<IActionResult> SortByVolume24h()
        {
            var sortedData = await _cryptoService.SortByVolume24hDescending();
            return Ok(sortedData);
        }

        [HttpGet("search-crypto")]
        public async Task<IActionResult> SearchCrypto([FromQuery] string searchTerm)
        {
            try
            {
                var searchRsult = await _cryptoService.SearchCrypto(searchTerm);
                return Ok(searchRsult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred: " + ex.ToString() });
            }
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetCryptoBySymbol(string symbol)
        {
            try
            {
                var cryptoResult = await _cryptoService.GetCryptoBySymbol(symbol);

                if (cryptoResult == null)
                {
                    return NotFound("Cryptocurrency not found.");
                }

                return Ok(cryptoResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred: " + ex.ToString() });
            }
        }
    }
}

