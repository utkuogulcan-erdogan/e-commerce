using Bussiness.Abstract;
using Entities.Concrete;
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
        public async Task<IActionResult> GetAll()
        {
            var results = await _userService.GetAll();
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByMail([FromRoute] string email)
        {
            var results = await _userService.GetByMail(email);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] User user)
        {
            var results = await _userService.Add(user);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] User user)
        {
            var results = await _userService.Update(user);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var results = await _userService.Delete(id);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
    }
}
