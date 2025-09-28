using Bussiness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAllBasketsDetailed()
        {
            var results = await _basketService.GetAllBasketsDetailedAsync();
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var results = await _basketService.GetDetailedBasketByIdAsync(id);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
        {
            var results = await _basketService.GetDetailedBasketByUserIdAsync(userId);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }

        [HttpPost("users/{userId}/products/{productId}/quantity/{quantity}")]
        public async Task<IActionResult> AddProductToBasketAsync([FromRoute] Guid userId, [FromRoute] Guid productId, [FromRoute] int quantity)
        {
            var results = await _basketLineService.AddProductToBasketAsync(userId, productId, quantity);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);

        }
        [HttpDelete("users/{userId}/products/{productId}")]
        public async Task<IActionResult> RemoveProductFromBasketAsync([FromRoute] Guid userId, [FromRoute] Guid productId)
        {
            var results = await _basketLineService.RemoveProductFromBasketAsync(userId, productId);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpPut("users/{userId}/products/{productId}/quantity/{quantity}")]
        public async Task<IActionResult> UpdateProductQuantityAsync([FromRoute] Guid userId, [FromRoute] Guid productId, [FromRoute] int quantity)
        {
            var results = await _basketLineService.UpdateProductQuantityAsync(userId, productId, quantity);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
    }
}
