using System.Text.Json;
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
        private readonly RabbitMQService _rabbitMQService;

        public CryptoService(HttpClient httpClient, IConfiguration configuration, CryptoDbContext context, RabbitMQService rabbitMQService)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ExternalApiSettings:ApiKey"];
            _baseUrl = configuration["ExternalApiSettings:BaseUrl"];
            _context = context;
            _rabbitMQService = rabbitMQService;
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
                    price = RoundToTwoDecimalPlaces(freshData.quote.usd.price),
                    marketCap = RoundToTwoDecimalPlaces(freshData.quote.usd.market_cap),
                    volume24h = RoundToTwoDecimalPlaces(freshData.quote.usd.volume_24h),
                    percentChange24h = RoundToTwoDecimalPlaces(freshData.quote.usd.percent_change_24h),
                    circulatingSupply = RoundToTwoDecimalPlaces(freshData.circulating_supply),
                    cmcRank = freshData.cmc_rank,
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
                        existingData.circulatingSupply = freshData.circulatingSupply;
                        existingData.cmcRank = freshData.cmcRank;
                        existingData.lastUpdated = DateTime.Now;
                    }
                }

                await _context.SaveChangesAsync();

                // Serialize the list of CryptoData to JSON
                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(freshDataList);

                // Publish the JSON data to RabbitMQ
                _rabbitMQService.PublishMessage(jsonData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private double RoundToTwoDecimalPlaces(double? value)
        {
            const double threshold = 0.01; 

            if (value.HasValue)
            {
                // Check if the absolute value is below the threshold
                if (Math.Abs(value.Value) < threshold)
                {
                    // Round to a non-zero value
                    return Math.Round(value.Value, 10, MidpointRounding.AwayFromZero);
                }

                // Round the value to two decimal places
                return Math.Round(value.Value, 2, MidpointRounding.AwayFromZero);
            }

            return 0;
        }

        private bool IsDataStale(DateTime lastUpdated)
        {
            // Define logic to check if the data is stale
            return (DateTime.Now - lastUpdated) > TimeSpan.FromMinutes(60);
        }

        public async Task<List<CryptoData>> SortByPriceChangeDescending()
        {
            var sortedData = _context.CryptoData.OrderByDescending(crypto => crypto.percentChange24h).ToList();
            return sortedData;
        }

        public async Task <List<CryptoData>> SortByVolume24hDescending()
        {
            var sortedData = _context.CryptoData.OrderByDescending(crypto => crypto.volume24h).ToList();
            return sortedData;
        }

        public async Task<List<CryptoData>> SearchCrypto(string searchTerm)
        {
            try
            {
                var searchResult = await _context.CryptoData.Where
                    (crypto => crypto.name.Contains(searchTerm) || crypto.symbol.Contains(searchTerm)).ToListAsync();
                return searchResult;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CryptoData> GetCryptoBySymbol(string symbol)
        {
            try
            {
                var cryptoResult = await _context.CryptoData.FirstOrDefaultAsync(c => c.symbol == symbol);
                return cryptoResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

