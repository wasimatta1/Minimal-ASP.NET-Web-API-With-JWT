using Microsoft.AspNetCore.Mvc;
using Minimal_ASP.NET_With_JWT.DTO;
using Minimal_ASP.NET_With_JWT.JWT_Token;

namespace Minimal_ASP.NET_With_JWT
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserTokenDto user)
        {
            var tokens = _jwtTokenService.GenerateToken(user);
            if (tokens == null)
            {
                return Unauthorized("User Not Sign UP");
            }
            return Ok(tokens);
        }

        [HttpPost("validate")]
        public IActionResult ValidateToken([FromBody] string token)
        {
            if (!_jwtTokenService.ValidateToken(token))
            {
                return Unauthorized("UnValidateToken");
            }
            return Ok("ValidateToken");
        }
    }
}
