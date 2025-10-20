using Bussiness.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        public UsersController(IUserService userService) {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var results = await _userService.GetAllAsync();
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByMailAsync(string email)
        {
            var results = await _userService.GetByMailAsync(email);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(UserAddDto user)
        {
            var results = await _userService.AddAsync(user);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UserUpdateDto user)
        {
            var results = await _userService.UpdateAsync(id, user);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var results = await _userService.DeleteAsync(id);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
    }
}
