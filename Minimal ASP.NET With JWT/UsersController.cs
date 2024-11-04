using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minimal_ASP.NET_With_JWT.Entities;

namespace Minimal_ASP.NET_With_JWT
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersServices;

        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usersServices.GetAll());
        }
    }
}
