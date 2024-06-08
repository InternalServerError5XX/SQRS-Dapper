using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Relations.Application.Services.UserService;
using Relations.Domain.DTO.UserDto;
using Relations.Domain.Identity.DTO.Auth;
using Relations.Domain.Identity.Static;
using System.IdentityModel.Tokens.Jwt;

namespace Relations.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IdentityController(
        UserManager<ApplicationUser> userManager, IUserService userService) : ControllerBase
    {     
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> UserId()
        {
            return Ok(await userService.GetUserId());
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] LoginDto user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Wrong input");

                var appUser = await userManager.FindByEmailAsync(user.Email);

                if (appUser == null)
                    return NotFound("User does not exist");

                if (!await userManager.CheckPasswordAsync(appUser, user.Password))
                    return BadRequest("Invalid password");

                var token = await userService.GenerateJwt(appUser);
                var tokenValue = new JwtSecurityTokenHandler().WriteToken(token.Token);

                CookieOptions cookieOptions = new()
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    Expires = token.Expiration
                };

                Response.Cookies.Append(
                    "token",
                    tokenValue,
                    cookieOptions);

                return Ok(new
                {
                    Token = tokenValue,
                    Expires = token.Expiration
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp([FromBody] SignupDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Wrong input");

                var appUser = await userManager.FindByEmailAsync(dto.Email);

                if (appUser != null)
                    return NotFound("User is already exists");

                var user = new ApplicationUser
                {
                    Email = dto.Email,
                    UserName = dto.UserName,
                };

                var response = await userManager.CreateAsync(user, dto.Password);

                if (!response.Succeeded)
                {
                    var errorsMessages =
                        response.Errors.Aggregate("", (current, error) => current + " " + error.Description);

                    return BadRequest(errorsMessages);
                }

                await userManager.AddToRoleAsync(user, DefaultRoles.User);

                var newUser = new LoginDto
                {
                    Email = dto.Email,
                    Password = dto.Password
                };

                return Ok(await Login(newUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
