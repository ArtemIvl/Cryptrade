using System;
using PortfolioManagement.Services;
using Microsoft.AspNetCore.Mvc;
using PortfolioManagement.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpGet]
        public IActionResult GetPortfolioById()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var portfolio = _portfolioService.GetPortfolioById(Convert.ToInt32(userId));

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
                var modelUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _portfolioService.CreatePortfolio(model, Convert.ToInt32(modelUserId));
                return Ok("Creation successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpPut]
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

        [HttpDelete]
        public IActionResult DeletePortfolio(int portfolioId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _portfolioService.DeletePortfolio(portfolioId, Convert.ToInt32(userId));
                return Ok("Portfolio  deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete portfolio: {ex.Message}");
            }
        }

        [HttpGet("total-value")]
        public async Task<IActionResult> GetTotalValue(int portfolioId)
        {
            try
            {
                var portfolioData = await _portfolioService.GetTotalValue(portfolioId);

                if (portfolioData != null)
                {
                    return Ok(portfolioData);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Getting data failed: {ex.Message}");
            }
        }
    }
}

