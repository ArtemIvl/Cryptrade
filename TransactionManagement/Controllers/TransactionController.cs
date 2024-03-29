﻿using TransactionManagement.Entity;
using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Services;
using TransactionManagement.Models;

namespace TransactionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
	{

        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public IActionResult CreateTransaction([FromBody] Transaction model)
        {
            try
            {
                _transactionService.AddTransaction(model);
                return Ok("Creation successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions(int portfolioId)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByPortfolioId(portfolioId);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpGet("assets")]
        public async Task<IActionResult> GetAllAssets(int portfolioId)
        {
            try
            {
                var assets = await _transactionService.GetAssetsByPortfolioId(portfolioId);
                return Ok(assets);
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpGet("trans-asset")]
        public async Task<IActionResult> GetTransactionsByAsset(int portfolioId, string assetName)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByAsset(portfolioId, assetName);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult DeleteTransaction(int transactionId)
        {
            try
            {
                _transactionService.DeleteTransaction(transactionId);
                return Ok("Transaction deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete transaction: {ex.Message}");
            }
        }


        [HttpDelete("byportfolio")]
        public IActionResult DeleteTransactionByPortfolioId(int portfolioId)
        {
            try
            {
                _transactionService.DeleteTransactionsByPortfolioId(portfolioId);
                return Ok("Transaction deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete transaction: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult UpdateTransactionData([FromBody] TransactionDataModel model, int transactionId)
        {
            try
            {
                _transactionService.UpdateTransaction(model, transactionId);
                return Ok("Transaction data updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update transaction data: {ex.Message}");
            }
        }
    }
}

