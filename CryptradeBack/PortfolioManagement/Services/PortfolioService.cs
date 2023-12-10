using System;
using System.Security.Claims;
using PortfolioManagement.Data;
using PortfolioManagement.Entity;
using PortfolioManagement.Models;

namespace PortfolioManagement.Services
{
	public class PortfolioService
	{
        private readonly PortfolioDbContext _context;

        public PortfolioService(PortfolioDbContext context)
        {
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

