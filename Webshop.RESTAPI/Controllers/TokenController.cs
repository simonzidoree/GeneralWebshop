using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webshop.Core.ApplicationService;
using Webshop.Core.Entities;
using Webshop.RESTAPI.Helpers.HelperInterfaces;

namespace Webshop.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _Service;
        private readonly IAuthenticationHelper authenticationHelper;

        public TokenController(IUserService service, IAuthenticationHelper authService)
        {
            _Service = service;
            authenticationHelper = authService;
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginInput model)
        {
            var user = _Service.GetAll().FirstOrDefault(u => u.Username == model.Username);


            // check if username exists
            if (user == null)
            {
                return Unauthorized();
            }

            // check if password is correct
            if (!authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }
    }
}