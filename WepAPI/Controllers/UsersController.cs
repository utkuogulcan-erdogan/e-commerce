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
        [HttpGet("getbymail")]
        public async Task<IActionResult> GetByMail(string email)
        {
            var results = await _userService.GetByMail(email);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(User user)
        {
            var results = await _userService.Add(user);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(User user)
        {
            var results = await _userService.Update(user);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
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
