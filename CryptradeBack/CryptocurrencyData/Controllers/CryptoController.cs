using System;
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

        [HttpGet("get-crypto-data")]
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
    }
}

