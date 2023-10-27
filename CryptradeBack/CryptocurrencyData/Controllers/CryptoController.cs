using System;
using CryptocurrencyData.Data;
using CryptocurrencyData.Models;
using CryptocurrencyData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
	{
        private readonly CryptoService _cryptoService;
        private readonly CryptoDbContext _context;
        private bool hasFetchedFreshData = false;

        public CryptoController(CryptoService cryptoService, CryptoDbContext context)
        {
            _cryptoService = cryptoService;
            _context = context;
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

        [HttpGet("cache-crypto-data")]
        public async Task<IActionResult> CachedCryptoData()
        {
            try
            {
                // Check if cached data exists and if it's stale
                var freshDataList = await _cryptoService.GetCryptoDataAsync();

                // Check if the fetched data is not empty
                if (freshDataList == null || freshDataList.Count == 0)
                {
                    return Ok("No fresh data available.");
                }

                foreach (var freshData in freshDataList)
                {
                    // Check if cached data exists and if it's stale
                    var existingData = await _context.CryptoData.FirstOrDefaultAsync(d => d.symbol == freshData.symbol);
                    if (existingData != null && !IsDataStale(existingData.lastUpdated))
                    {
                        // Data for this cryptocurrency is already up to date.
                        continue;
                    }

                    if (existingData == null)
                    {
                        _context.CryptoData.Add(new CryptoData
                        {
                            name = freshData.name,
                            symbol = freshData.symbol,
                            price = freshData.quote.usd.price,
                            marketCap = freshData.quote.usd.market_cap,
                            volume24h = freshData.quote.usd.volume_24h,
                            percentChange24h = freshData.quote.usd.percent_change_24h,
                            lastUpdated = DateTime.Now
                        });
                    }
                    else
                    {
                        existingData.name = freshData.name;
                        existingData.symbol = freshData.symbol;
                        existingData.price = freshData.quote.usd.price;
                        existingData.marketCap = freshData.quote.usd.market_cap;
                        existingData.volume24h = freshData.quote.usd.volume_24h;
                        existingData.percentChange24h = freshData.quote.usd.percent_change_24h;
                        existingData.lastUpdated = DateTime.Now;
                    }
                }

                await _context.SaveChangesAsync();

                return Ok("Data has been updated and cached.");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, new { message = "An error occurred while caching data: " + ex.ToString() });
            }
        }

        [HttpGet("get-cached-crypto-data")]
        public async Task<IActionResult> GetCachedCryptoData()
        {
            try
            {
                var cachedData = await _context.CryptoData.ToListAsync();

                if (cachedData.Count == 0 && !hasFetchedFreshData)
                {
                    // Call the CachedCryptoData method to fetch and cache fresh data
                    await CachedCryptoData();
                    hasFetchedFreshData = true;
                }

                cachedData = await _context.CryptoData.ToListAsync();

                if (cachedData.Count == 0)
                {
                    return BadRequest("No data available.");
                }

                return Ok(cachedData);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, new { message = "An error occurred: " + ex.ToString() });
            }
        }

        private bool IsDataStale(DateTime lastUpdated)
        {
            // Define your own logic to check if the data is stale, e.g., if it's older than a certain threshold
            var threshold = TimeSpan.FromMinutes(60); // Example: consider data stale if older than 30 minutes
            return DateTime.Now - lastUpdated > threshold;
        }
    }
}

