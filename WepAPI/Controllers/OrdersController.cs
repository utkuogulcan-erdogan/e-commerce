using Bussiness.Abstract;
using Entities.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;
        IOrderAddressService _orderAddressService;
        public OrdersController(IOrderService orderService, IOrderAddressService orderAddressService)
        {
            _orderService = orderService;
            _orderAddressService = orderAddressService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var results = await _orderService.GetAllAsync();
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var results = await _orderService.GetOrderByIdAsync(id);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }

        [HttpPost("users/{userId}/orders")]
        public async Task<IActionResult> CreateOrderAsync(Guid userId, OrderCreateDto dto)
        {
            var order = await _orderService.CreateOrderAsync(userId, dto);
            if (order.Success)
            {
                return Ok(order);
            }
            return BadRequest(order);
        }

        [HttpPost("orders/{userId}/orders/payments")]
        public async Task<IActionResult> CreatePaymentAsync(Guid userId, OrderPaymentDto dto)
        {
            var payment = await _orderService.CreatePaymentAsync(userId, dto);
            if (payment.Success)
            {
                return Ok(payment);
            }
            return BadRequest(payment);
        }

        [HttpPut("orders/{userId}/orders/status")]
        public async Task<IActionResult> UpdateOrderStatusAsync(Guid userId, OrderUpdateStatusDto dto)
        {
            var result = await _orderService.UpdateOrderStatusAsync(userId,dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
