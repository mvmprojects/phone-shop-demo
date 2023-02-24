using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Phoneshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        public class AuthenticationRequestBody
        {
            [Required]
            public string UserName { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string EmailAddress { get; set; }
        }

        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticationController(IConfiguration config,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _config = config ??
                throw new ArgumentNullException();
            _userManager = userManager ?? throw new ArgumentNullException();
            _signInManager = signInManager ?? throw new ArgumentNullException();
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<string>> Login(
            [FromForm]
            AuthenticationRequestBody requestBody)
        {
            // step 1: validate username and pw
            var user = await ValidateUserCredentials(requestBody);

            if (string.IsNullOrWhiteSpace(user?.UserName)) return Unauthorized();

            // step 2: create token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    _config["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim(ClaimTypes.Name, user.UserName));
            claimsForToken.Add(new Claim(ClaimTypes.Email, user.Email));
            claimsForToken.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(
            [FromForm]
            AuthenticationRequestBody requestBody)
        {
            var user = new IdentityUser
            {
                UserName = requestBody.UserName,
                Email = requestBody.EmailAddress
            };

            var result = await _userManager.CreateAsync(user, requestBody.Password);
            if (!result.Succeeded) return StatusCode(500);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return Ok(new { success = true, confirmationCode = code });
        }

        private async Task<IdentityUser> ValidateUserCredentials(
            AuthenticationRequestBody requestBody)
        {
            // use empty name as a fail condition
            var emptyUser = new IdentityUser() { UserName = "" };

            var result = await _signInManager.PasswordSignInAsync(
                requestBody.UserName,
                requestBody.Password,
                false,
                true);
            if (!result.Succeeded) return emptyUser;

            var user = await _userManager.FindByEmailAsync(requestBody.EmailAddress);
            return user;
        }
    }
}
