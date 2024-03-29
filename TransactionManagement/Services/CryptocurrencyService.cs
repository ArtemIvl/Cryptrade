﻿using System;
using TransactionManagement.Models;

namespace TransactionManagement.Services
{
    public class CryptocurrencyService
    {
        private readonly RabbitMQConsumer _rabbitMQConsumer;

        public CryptocurrencyService(RabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
        }

        public async Task<Cryptocurrency> GetCryptocurrenciesUsedInTransactions(string cryptoName)
        {
            try
            {
                _rabbitMQConsumer.StartConsuming();
                // Retrieve the cached cryptocurrencies from RabbitMQConsumer
                var cryptocurrencies = await _rabbitMQConsumer.GetCachedCryptocurrenciesAsync(cryptoName);
                return cryptocurrencies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}