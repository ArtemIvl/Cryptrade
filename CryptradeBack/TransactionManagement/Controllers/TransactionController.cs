using System;
using TransactionManagement.Entity;
using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Services;

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
        public IActionResult CreatePortfolio([FromBody] Transaction model)
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
    }
}

