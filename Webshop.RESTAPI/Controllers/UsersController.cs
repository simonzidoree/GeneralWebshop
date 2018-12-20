using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webshop.Core.ApplicationService;
using Webshop.Core.Entities;

namespace Webshop.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        private IUserService UserService { get; }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] UserModel user)
        {
            CreatePasswordHash(user.Password, out var hash, out var salt);
            return Ok(UserService.Create(new User
            {
                Username = user.Username,
                PasswordHash = hash,
                PasswordSalt = salt,
                IsAdmin = user.IsAdmin
            }));
        }

        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public class UserModel
        {
            public bool IsAdmin { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }
    }
}