using Api.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Api.Models.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
                private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
                                         SignInManager<ApplicationUser> signInManager,
                                         IConfiguration configuration, IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        // POST api/authentication/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "User registered successfully" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid data.");
        }




        // PUT api/authentication/update
        
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Verify if the provided current password is correct
            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!passwordCheck)
            {
                return BadRequest(new { Message = "Current password is incorrect." });
            }

            // Update email if provided
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
                var emailResult = await _userManager.UpdateAsync(user);
                if (!emailResult.Succeeded)
                {
                    return BadRequest(emailResult.Errors);
                }
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    return BadRequest(passwordResult.Errors);
                }
            }

            return Ok(new { Message = "User updated successfully" });
        }





        // POST api/authentication/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // Create the claims for the JWT token
                var claims = new[]
                {

                new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // Add the user's ID
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

                // Retrieve secret key from appsettings.json
                var secretKey = _configuration["Jwt:Secret"];
                var key = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));


                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(int.Parse(_configuration["Jwt:ExpiryDays"])),
                    signingCredentials: creds
                );


                // Generate a 6-digit number with each digit randomized
                var random = new Random();
                var randomNumber = string.Concat(Enumerable.Range(0, 6).Select(_ => random.Next(0, 10).ToString()));
                
                _memoryCache.Set(user.Id, randomNumber, TimeSpan.FromMinutes(5));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized();
        }
    }
}
