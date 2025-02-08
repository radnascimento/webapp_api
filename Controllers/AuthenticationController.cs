using Api.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Api.Models.Dtos;
using Microsoft.Extensions.Caching.Memory;
using Api.Helpers;
using System.Net;

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
                // Check if the username already exists
                var existingUserByUsername = await _userManager.FindByNameAsync(model.UserName);
                if (existingUserByUsername != null)
                {
                    return BadRequest("Username already exists.");
                }
                try
                {

                    // Check if the email already exists
                    var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
                    if (existingUserByEmail != null)
                    {
                        return BadRequest("Email already exists.");
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

                // Create the new user
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                var emailTemplate = new EmailHelper();
                var welcomeEmailBody = await emailTemplate.LoadEmailTemplateAsync("welcomeEmailBody.html");

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodedToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(token));
                    var baseUrl = $"{Request.Scheme}://{Request.Host}";
                    var confirmationLink = $"{baseUrl}/api/Authentication/confirm-email?userId={user.Id}&token={encodedToken}";
                    var emailHelper = new EmailHelper("rogerfin.somee@gmail.com", "tcmo bywt hbgk mqnh");

                    try
                    {
                        bool emailSent = await emailHelper.SendEmailAsync(model.Email, "Spaced Study - Welcome", welcomeEmailBody.Replace("{UserName}", model.UserName).Replace("{confirmationLink}", confirmationLink));
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    return Ok(new { Message = "User registered successfully" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid data.");
        }


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return BadRequest("Invalid email confirmation request");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            var decodedToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
                return Ok("Email confirmed successfully!");

            return BadRequest($"Email confirmation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return Unauthorized("Email not confirmed. Please check your email.");


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
                expires: DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:ExpiryDays"])),
                signingCredentials: creds
            );

            // Generate a 6-digit verification code
            var random = new Random();
            var verificationCode = string.Concat(Enumerable.Range(0, 6).Select(_ => random.Next(0, 10).ToString()));

            // Store the verification code in memory cache for 5 minutes
            _memoryCache.Set(user.Id, verificationCode, TimeSpan.FromMinutes(5));

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                verificationCode = verificationCode, // Send verification code if needed
                message = "Login successful."
            });
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            //var resetLink = $"{baseUrl}/api/Authentication/reset-password?userId={user.Id}&token={encodedToken}";
            var resetLink = $"{model.Url}?userId={user.Id}&token={encodedToken}";

            var emailHelper = new EmailHelper("rogerfin.somee@gmail.com", "tcmo bywt hbgk mqnh"); 
            var emailBody = $"Click <a href='{resetLink}'>here</a> to reset your password.";

            await emailHelper.SendEmailAsync(model.Email, "Reset Password", emailBody);

            return Ok("Password reset link has been sent to your email.");
        }

        //[HttpGet("reset-password")]
        //public async Task<IActionResult> ResetPasswordPage([FromQuery] Guid userId, [FromQuery] string token)
        //{
        //    // Validate the token here if needed (e.g., check if it's expired, etc.)
        //    // If validation succeeds, show a page (or return a success response)

        //    if (string.IsNullOrEmpty(token) )
        //    {
        //        return BadRequest("Invalid password reset request.");
        //    }

        //    var user = await _userManager.FindByIdAsync(userId.ToString());
        //    if (user == null)
        //    {
        //        return NotFound("User not found.");
        //    }


        //    return Ok(user); // You could render a page or return a success response.
        //}


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (string.IsNullOrEmpty(model.UserId) || string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.NewPassword))
            {
                return BadRequest("Invalid password reset request.");
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(model.Token));
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password has been reset successfully.");
            }

            return BadRequest("result.Errors");
        }


    }
}
