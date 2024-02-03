using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLSV.Entities;

namespace QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly StudentContext _context;

        public LoginsController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            var logins = await _context.Logins.ToListAsync();
            return Ok(logins);
        }

        [HttpPost("LoginVer")]
        public async Task<IActionResult> LoginVer(Login request)
        {
            var user = await _context.Logins.FirstOrDefaultAsync(u => u.Login1 == request.Login1);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            if (!VerifyPassword(request.Password))
            {
                return Unauthorized("Wrong user or password");
            }
            return Ok("Welcome");
        }

        private bool LoginExists(string id)
        {
            return _context.Logins.Any(e => e.Login1 == id);
        }

        private bool VerifyPassword(string password)
        {
            return _context.Logins.Any(e => e.Password == password);
        }
    }
}
