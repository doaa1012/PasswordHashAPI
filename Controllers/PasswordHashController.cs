using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace PasswordHashAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordHashController : ControllerBase
    {
        private readonly PasswordHasher<string> _passwordHasher;

        public PasswordHashController()
        {
            _passwordHasher = new PasswordHasher<string>();
        }

        // POST api/passwordhash/hash
        [HttpPost("hash")]
        public IActionResult HashPassword([FromBody] PasswordModel model)
        {
            var hashedPassword = _passwordHasher.HashPassword(null, model.Password);
            return Ok(hashedPassword);
        }

        // POST api/passwordhash/verify
        [HttpPost("verify")]
        public IActionResult VerifyPassword([FromBody] PasswordVerificationModel model)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, model.HashedPassword, model.Password);
            if (result == PasswordVerificationResult.Success)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }

    public class PasswordModel
    {
        public string Password { get; set; }
    }

    public class PasswordVerificationModel
    {
        public string HashedPassword { get; set; }
        public string Password { get; set; }
    }
}
