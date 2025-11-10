using Bussiness.Abstract;
using Entities.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WepAPI.Extensions;

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
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var results = await _orderService.GetAllAsync(cancellationToken);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var results = await _orderService.GetOrderByIdAsync(id, cancellationToken);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }

        [HttpPost("user/orders")]
        public async Task<IActionResult> CreateOrderAsync(OrderCreateDto dto, CancellationToken cancellationToken)
        {
            Guid userId = HttpContext.GetUserId();
            var order = await _orderService.CreateOrderAsync(userId, dto, cancellationToken);
            if (order.Success)
            {
                return Ok(order);
            }
            return BadRequest(order);
        }

        [HttpPost("user/order/payments")]
        public async Task<IActionResult> CreatePaymentAsync(OrderPaymentDto dto, CancellationToken cancellationToken)
        {
            Guid userId = HttpContext.GetUserId();
            var payment = await _orderService.CreatePaymentAsync(userId, dto, cancellationToken);
            if (payment.Success)
            {
                return Ok(payment);
            }
            return BadRequest(payment);
        }

        [HttpPut("user/order/status")]
        public async Task<IActionResult> UpdateOrderStatusAsync(OrderUpdateStatusDto dto, CancellationToken cancellationToken)
        {
            Guid userId = HttpContext.GetUserId();
            var result = await _orderService.UpdateOrderStatusAsync(userId, dto, cancellationToken);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
