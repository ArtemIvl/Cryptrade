using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CryptocurrencyData.Data;
using CryptocurrencyData.Entity;
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

        public async Task<List<CryptoData>> GetCryptoDataAsync()
        {
            try
            {
                var cachedData = await _context.CryptoData.ToListAsync();
                if (cachedData != null && cachedData.Any(d => !IsDataStale(d.lastUpdated)))
                {
                    return cachedData.ToList();
                } else
                {
                    var freshDataList = await GetCryptoDataFromApi();
                    await CacheCryptoData(freshDataList);
                    return freshDataList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // take data from cmc api
        public async Task<List<CryptoData>> GetCryptoDataFromApi()
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

                var freshDataList = cryptoResponse?.Data.Select(freshData => new CryptoData
                {
                    name = freshData.name,
                    symbol = freshData.symbol,
                    price = freshData.quote.usd.price,
                    marketCap = freshData.quote.usd.market_cap,
                    volume24h = freshData.quote.usd.volume_24h,
                    percentChange24h = freshData.quote.usd.percent_change_24h,
                    lastUpdated = DateTime.Now
                }).ToList();

                return freshDataList;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw ex;
            }
        }

        // cache data to local db
        public async Task CacheCryptoData(List<CryptoData> freshDataList)
        {
            try
            {
                foreach (var freshData in freshDataList)
                {
                    var existingData = await _context.CryptoData.FirstOrDefaultAsync(d => d.symbol == freshData.symbol);

                    if (existingData == null)
                    {
                        _context.CryptoData.Add(freshData);
                    }
                    else
                    {
                        // update existing data
                        existingData.name = freshData.name;
                        existingData.symbol = freshData.symbol;
                        existingData.price = freshData.price;
                        existingData.marketCap = freshData.marketCap;
                        existingData.volume24h = freshData.volume24h;
                        existingData.percentChange24h = freshData.percentChange24h;
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

