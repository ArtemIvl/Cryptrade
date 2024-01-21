using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TradingManagement.Entity;
using TradingManagement.Services;

namespace TradingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingController : ControllerBase
    {
        private readonly OrderService _orderService;

        public TradingController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order model)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                model.userId = Convert.ToInt32(userId);
                _orderService.AddOrder(model);
                return Ok("Creation successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPositions()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var orders = await _orderService.GetOrdersByUserId(Convert.ToInt32(userId));
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetFinishedOrders()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var finishedOrders = await _orderService.GetFinishedOrdersByUserId(Convert.ToInt32(userId));
                return Ok(finishedOrders);
            }
            catch (Exception ex)
            {
                return BadRequest("Getting finished orders failed: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> OpenOrderAfterWaiting(bool isOpen, int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _orderService.OpenOrderByIdAndUserId(id, Convert.ToInt32(userId), isOpen);
                return Ok("Order has been successfully opened!");

            }
            catch (Exception ex)
            {
                return BadRequest("Opening order failed: " + ex.Message);
            }
        }

        [HttpPut("close")]
        public async Task<IActionResult> CloseOrder([FromBody] Order model)
        {
            if (model.isOpen == false)
            {
                return BadRequest("Order is already closed");
            } else
            {
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    _orderService.CloseOrderByIdAndUserId(model.id, Convert.ToInt32(userId));
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest("CLosing order failed: " + ex.Message);
                }
            }
        }
    }
}

