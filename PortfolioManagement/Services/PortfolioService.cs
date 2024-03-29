﻿using System;
using System.Security.Claims;
using PortfolioManagement.Data;
using PortfolioManagement.Entity;
using PortfolioManagement.Models;

namespace PortfolioManagement.Services
{
    public class PortfolioService
    {
        private readonly PortfolioDbContext _context;
        private readonly RabbitMQConsumer _rabbitMQConsumer;
        private Dictionary<int, double> _portfolioTotalValues = new Dictionary<int, double>();

        public PortfolioService(PortfolioDbContext context, RabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
            _context = context;
        }

        public PortfolioDataModel GetPortfolioById(int userId)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(p => p.userId == userId);

            if (portfolio != null)
            {
                var portfolioData = new PortfolioDataModel
                {
                    name = portfolio.name,
                    description = portfolio.description,
                    totalValue = portfolio.totalValue,
                    userId = portfolio.userId,
                    id = portfolio.id
                };

                return portfolioData;
            }

            return null;
        }

        public void CreatePortfolio(PortfolioDataModel model, int modelUserId)
        {
            var newPortfolio = new Portfolio
            {
                name = model.name,
                description = model.description,
                userId = modelUserId,
                totalValue = model.totalValue
            };

            _context.Portfolios.Add(newPortfolio);
            _context.SaveChanges();
        }

        public void UpdatePortfolio(int portfolioId, string newName, string newDescription)
        {
            var portfolio = _context.Portfolios.Find(portfolioId);

            if (portfolio != null)
            {
                portfolio.name = newName;
                portfolio.description = newDescription;
                _context.SaveChanges();
            }
        }

        public async Task<TotalValueModel> GetTotalValue(int portfolioId)
        {
            try
            {
                _rabbitMQConsumer.StartConsuming(portfolioId);
                var totalValue = await _rabbitMQConsumer.GetTotalValueByPortfolioId(portfolioId);
                var profitLoss = await _rabbitMQConsumer.GetProfitLossByPortfolioId(portfolioId);
                var bestPerformer = await _rabbitMQConsumer.GetBestPerformerByPortfolioId(portfolioId);
                var worstPerformer = await _rabbitMQConsumer.GetWorstPerformerByPortfolioId(portfolioId);
                var portfolioData = new TotalValueModel
                {
                    totalValue = totalValue,
                    profitLoss = profitLoss,
                    bestPerformer = bestPerformer,
                    worstPerformer = worstPerformer,
                    portfolioId = portfolioId,
                };
                return portfolioData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePortfolio(int portfolioId, int userId)
        {
            var portfolio = _context.Portfolios.FirstOrDefault(p => p.id == portfolioId && p.userId == userId);
            if (portfolio != null)
            {
                _context.Portfolios.Remove(portfolio);
                _context.SaveChanges();
            }
        }
    }
}
