using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Supernova.Api.src.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Supernova.Api.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AppUser loginModel)
        {
            if (IsValidUser(loginModel))
            {
                var token = GenerateToken();
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private bool IsValidUser(AppUser loginModel)
        {
            // Kullanıcı doğrulama işlemini burada yapın (örneğin, veritabanı kontrolü)
            return loginModel.Username == "test" && loginModel.Password == "password";
        }

        private string GenerateToken()
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, "user_id"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
