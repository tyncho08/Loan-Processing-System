using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LPSystemWebAPICore.Data;
using LPSystemWebAPICore.Models;

namespace LPSystemWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAuthController : ControllerBase
    {
        private readonly LPSystemContext _context;

        public LoginAuthController(LPSystemContext context)
        {
            _context = context;
        }

        // POST: api/LoginAuth
        [HttpPost]
        public async Task<ActionResult<UserTable>> PostLogin([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.useremail) || string.IsNullOrEmpty(loginRequest.userpass))
            {
                return BadRequest("Email and password are required");
            }

            var user = await _context.UserTables
                .FirstOrDefaultAsync(u => u.UserEmail == loginRequest.useremail && u.UserPass == loginRequest.userpass);

            if (user == null)
            {
                return null; // Frontend expects null for invalid login
            }

            return user;
        }
    }

    public class LoginRequest
    {
        public string useremail { get; set; } = string.Empty;
        public string userpass { get; set; } = string.Empty;
    }
}