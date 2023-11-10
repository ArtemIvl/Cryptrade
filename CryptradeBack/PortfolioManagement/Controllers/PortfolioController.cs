using System;
using PortfolioManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PortfolioManagement.Entity;
using PortfolioManagement.Models;

namespace PortfolioManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
	{
		private readonly PortfolioService _portfolioService;

		public PortfolioController(PortfolioService portfolioService)
		{
			_portfolioService = portfolioService;
		}


        [HttpGet("{userId}/{portfolioId}")]
        public IActionResult GetPortfolioById(int userId, int portfolioId)
        {
            try
            {
                var portfolio = _portfolioService.GetPortfolioById(portfolioId, userId);

                if (portfolio != null)
                {
                    return Ok(portfolio);
                } else
                {
                    return NotFound("Portfolio data not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Getting data failed: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreatePortfolio([FromBody] PortfolioDataModel model)
        {
            try
            {
                _portfolioService.CreatePortfolio(model);
                return Ok("Creation successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpPut("{portfolioId}")]
        public IActionResult UpdatePortfolio(int portfolioId, [FromBody] PortfolioDataModel model)
        {
            try
            {
                _portfolioService.UpdatePortfolio(portfolioId, model.name, model.description);
                return Ok("Portfolio data updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update portfolio data: {ex.Message}");
            }
        }

        [HttpDelete("{userId}/{portfolioId}")]
        public IActionResult DeletePortfolio(int userId, int portfolioId)
        {
            try
            {
                _portfolioService.DeletePortfolio(userId, portfolioId);
                return Ok("Portfolio  deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete portfolio: {ex.Message}");
            }
        }
    }
}

