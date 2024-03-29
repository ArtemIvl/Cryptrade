﻿using System;
using Microsoft.EntityFrameworkCore;
using TransactionManagement.Data;
using TransactionManagement.Entity;
using TransactionManagement.Models;

namespace TransactionManagement.Services
{
    public class TransactionService
    {
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly TransactionDbContext _context;
        private readonly CryptocurrencyService _cryptocurrencyService;

        public TransactionService(TransactionDbContext context, CryptocurrencyService cryptocurrencyService, RabbitMQPublisher rabbitMQPublisher)
        {
            _context = context;
            _cryptocurrencyService = cryptocurrencyService;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public void AddTransaction(Transaction model)
        {
            _context.Transactions.Add(model);
            _context.SaveChanges();
        }

        public async Task<List<Transaction>> GetTransactionsByPortfolioId(int portfolioId)
        {
            var transactions = await _context.Transactions.Where(t => t.portfolioId == portfolioId).ToListAsync();
            if (transactions != null)
            {
                return transactions;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Asset>> GetAssetsByPortfolioId(int portfolioId)
        {
            var transactions = await _context.Transactions.Where(t => t.portfolioId == portfolioId).ToListAsync();
            List<Cryptocurrency> cryptocurrencies = new List<Cryptocurrency>();

            foreach (var transaction in transactions)
            {
                cryptocurrencies.Add(await _cryptocurrencyService.GetCryptocurrenciesUsedInTransactions(transaction.cryptoName));
            }

            var assets = new List<Asset>();

            if (transactions != null && cryptocurrencies != null)
            {
                foreach (var transaction in transactions)
                {
                    var cryptoInfo = cryptocurrencies.FirstOrDefault(crypto => crypto.name == transaction.cryptoName);

                    if (cryptoInfo != null)
                    {
                        var amount = transaction.amount;
                        var buyPrice = transaction.price;
                        var currentPrice = cryptoInfo.price;
                        var profitLoss = (currentPrice - buyPrice) * amount;
                        var percentChange24h = cryptoInfo.percentChange24h;

                        var existingAsset = assets.FirstOrDefault(asset => asset.cryptoName == cryptoInfo.name);

                        if (existingAsset != null)
                        {
                            // If the asset already exists, update its values
                            existingAsset.amount += amount;
                            existingAsset.avgBuyPrice += buyPrice;
                            existingAsset.profitLoss += profitLoss;
                            existingAsset.numOfTransactions += 1; // Increment the transaction count
                        }
                        else
                        {
                            // If the asset doesn't exist, add a new entry
                            assets.Add(new Asset
                            {
                                cryptoName = cryptoInfo.name,
                                cryptoSymbol = cryptoInfo.symbol,
                                percentChange24h = percentChange24h,
                                amount = amount,
                                avgBuyPrice = buyPrice,
                                currentPrice = currentPrice,
                                profitLoss = profitLoss,
                                portfolioId = transaction.portfolioId,
                                numOfTransactions = 1 // Initialize the transaction count
                            });
                        }
                    }
                }

                // Calculate the average buy price for each asset
                assets.ForEach(asset => asset.avgBuyPrice = asset.avgBuyPrice / asset.numOfTransactions);
            }
            else
            {
                return null;
            }

            double totalvalue = 0;

            double profitloss = 0;

            Performer bestPerformer = null;
            Performer worstPerformer = null;

            foreach (Asset asset in assets)
            {
                totalvalue += asset.amount * asset.currentPrice;
                profitloss += asset.profitLoss;

                if (bestPerformer == null || asset.profitLoss > bestPerformer.profitLoss)
                {
                    bestPerformer = new Performer
                    {
                        cryptoName = asset.cryptoName,
                        cryptoSymbol = asset.cryptoSymbol,
                        profitLoss = asset.profitLoss
                    };
                }

                if (worstPerformer == null || asset.profitLoss < worstPerformer.profitLoss)
                {
                    worstPerformer = new Performer
                    {
                        cryptoName = asset.cryptoName,
                        cryptoSymbol = asset.cryptoSymbol,
                        profitLoss = asset.profitLoss
                    };
                }
            }
            Console.WriteLine("start publish");
            _rabbitMQPublisher.PublishMessage(totalvalue, profitloss, portfolioId, bestPerformer, worstPerformer);

            return assets;
        }

        public async Task<List<Transaction>> GetTransactionsByAsset(int portfolioId, string assetName)
        {

            var transactions = await _context.Transactions.Where(t => t.portfolioId == portfolioId && t.cryptoName == assetName).ToListAsync();
            if (transactions != null)
            {
                return transactions;
            }
            else
            {
                return null;
            }
        }

        public void UpdateTransaction(TransactionDataModel model, int transactionid)
        {
            var transaction = _context.Transactions.Find(transactionid);

            if (transaction != null)
            {
                transaction.createdAt = model.createdAt;
                transaction.amount = model.amount;
                transaction.price = model.price;
                _context.SaveChanges();
            }
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.id == id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Transaciton not found");
            }
        }

        public void DeleteTransactionsByPortfolioId(int portfolioId)
        {
            var transactions = _context.Transactions.Where(t => t.portfolioId == portfolioId).ToList();

            if (transactions.Any())
            {
                _context.Transactions.RemoveRange(transactions);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("No transactions found for the given portfolio id");
            }
        }
    }
}
