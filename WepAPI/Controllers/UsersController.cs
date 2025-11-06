using Bussiness.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var results = await _userService.GetAllAsync(cancellationToken);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByMailAsync(string email, CancellationToken cancellationToken)
        {
            var results = await _userService.GetByMailAsync(email, cancellationToken);
            if (results.Success)
            {
                return Ok(results.Data);
            }
            return BadRequest(results);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(UserAddDto user, CancellationToken cancellationToken)
        {
            var results = await _userService.AddAsync(user, cancellationToken);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UserUpdateDto user, CancellationToken cancellationToken)
        {
            var results = await _userService.UpdateAsync(id, user, cancellationToken);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var results = await _userService.DeleteAsync(id, cancellationToken);
            if (results.Success)
            {
                return Ok(results);
            }
            return BadRequest(results);
        }
    }
}
