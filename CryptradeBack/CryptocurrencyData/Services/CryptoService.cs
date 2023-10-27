using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CryptocurrencyData.Data;
using CryptocurrencyData.Entity;
using CryptocurrencyData.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptocurrencyData.Services
{
	public class CryptoService
	{
		private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly CryptoDbContext _context;

        public CryptoService(HttpClient httpClient, IConfiguration configuration, CryptoDbContext context)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ExternalApiSettings:ApiKey"];
            _baseUrl = configuration["ExternalApiSettings:BaseUrl"];
            _context = context;
        }


        // take data from cmc api
        public async Task<List<Cryptocurrency>> GetCryptoDataAsync()
        {
            try
            {
                var url = $"{_baseUrl}/cryptocurrency/listings/latest?CMC_PRO_API_KEY={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var cryptoResponse = JsonSerializer.Deserialize<CryptoApiResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return cryptoResponse?.Data;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw ex;
            }
        }

        // cache data from external api to local db
        public async Task CacheCryptoData(List<Cryptocurrency> freshDataList)
        {
            try
            {
                foreach (var freshData in freshDataList)
                {
                    var existingData = await _context.CryptoData.FirstOrDefaultAsync(d => d.symbol == freshData.symbol);
                    if (existingData != null && !IsDataStale(existingData.lastUpdated))
                    {
                        continue; // data is already up to date
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
                    } else
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsDataStale(DateTime lastUpdated)
        {
            // Define logic to check if the data is stale
            return (DateTime.Now - lastUpdated) > TimeSpan.FromMinutes(60);
        }
    }
}

