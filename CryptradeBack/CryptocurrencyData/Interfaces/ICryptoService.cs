using System;
using CryptocurrencyData.Entity;

namespace CryptocurrencyData.Interfaces
{
	public interface ICryptoService
	{
        Task<List<CryptoData>> GetCryptoDataAsync();
        Task<List<CryptoData>> GetCryptoDataFromApi();
        Task CacheCryptoData(List<CryptoData> freshDataList);
        Task<List<CryptoData>> SortByPriceChangeDescending();
        Task<List<CryptoData>> SortByVolume24hDescending();
        Task<List<CryptoData>> SearchCrypto(string searchTerm);
        Task<CryptoData> GetCryptoBySymbol(string symbol);
    }
}

