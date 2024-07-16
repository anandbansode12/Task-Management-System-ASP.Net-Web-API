using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TMS_Project.Data;
using TMS_Project.Settings;

namespace TMS_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthSettings _authSettings;
        private readonly TMSContext _context;

        public AuthController(IOptions<AuthSettings> authSettings, TMSContext context)
        {
            _authSettings = authSettings.Value ?? throw new ArgumentNullException(nameof(authSettings));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin model)
        {
            var user = _context.Users
                                .Include(u => u.Role)  
                                .SingleOrDefault(x => x.Username == model.Username && x.PasswordHash == model.PasswordHash);

            if (user == null)
                return Unauthorized();

            if (_authSettings == null || string.IsNullOrEmpty(_authSettings.Secret))
            {
                throw new InvalidOperationException("AuthSettings is not configured correctly.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.RoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Audience = _authSettings.Audience, 
                Issuer = _authSettings.Issuer, 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        public class UserLogin
        {
            public string Username { get; set; }
            public string PasswordHash { get; set; }
        }
    }
}
