using Bussiness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        IBasketService _basketService;
        IBasketLineService _basketLineService;
        public BasketsController(IBasketService basketService, IBasketLineService basketLineService)
        {
            _basketService = basketService;
            _basketLineService = basketLineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBasketsDetailedAsync(CancellationToken cancellationToken)
        {
            var results = await _basketService.GetAllBasketsDetailedAsync(cancellationToken);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var results = await _basketService.GetDetailedBasketByIdAsync(id, cancellationToken);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetByUserIdAsync(CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var results = await _basketService.GetDetailedBasketByUserIdAsync(userId, cancellationToken);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }

        [HttpPost("user/products/{productId}/quantity/{quantity}")]
        public async Task<IActionResult> AddProductToBasketAsync(Guid productId, int quantity, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var results = await _basketLineService.AddProductToBasketAsync(userId, productId, quantity, cancellationToken);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);

        }
        [HttpDelete("user/products/{productId}")]
        public async Task<IActionResult> RemoveProductFromBasketAsync(Guid productId, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var results = await _basketLineService.RemoveProductFromBasketAsync(userId, productId, cancellationToken);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpPut("users/products/{productId}/quantity/{quantity}")]
        public async Task<IActionResult> UpdateProductQuantityAsync(Guid productId, int quantity, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            var results = await _basketLineService.UpdateProductQuantityAsync(userId, productId, quantity, cancellationToken);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
    }
}
