using BookMarket.Api.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtManager manager;

        public TokenController(JwtManager manager)
        {
            this.manager = manager;
        }

        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest request)
        {
            MD5 hash = MD5.Create();
            byte[] pass = hash.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < pass.Length; i++)
            {
                strBuilder.Append(pass[i].ToString("x2"));
            }
            request.Password = strBuilder.ToString();

            var token = manager.MakeToken(request.Email, request.Password);
            if (token == null) return Unauthorized();
            return Ok(new { token });
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
