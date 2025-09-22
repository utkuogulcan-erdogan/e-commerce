using Bussiness.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _basketService.GetAll();
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("getallbasketsdetailed")]
        public async Task<IActionResult> GetAllBasketsDetailed()
        {
            var results = await _basketService.GetAllBasketsDetailedAsync();
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var results = await _basketService.GetById(id);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var results = await _basketService.GetByUserId(userId);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("getdetailedbasketbyuserid")]
        public async Task<IActionResult> GetDetailedBasketByUserId(Guid userId)
        {
            var results = await _basketService.GetDetailedBasketByUserIdAsync(userId);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(Guid userId)
        {
            var results = await _basketService.Add(userId);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(Entities.Concrete.Basket basket)
        {
            var results = await _basketService.Update(basket);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var results = await _basketService.Delete(id);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
    }
}
