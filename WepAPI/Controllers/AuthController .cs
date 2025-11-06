using Bussiness.Abstract;
using Entities.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ICustomAuthorizationService _customAuthorizationService;
        public AuthController(ICustomAuthorizationService customAuthorizationService)
        {
            _customAuthorizationService = customAuthorizationService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, CancellationToken cancellationToken)
        {
            var result = await _customAuthorizationService.Login(loginRequestDto, cancellationToken);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
