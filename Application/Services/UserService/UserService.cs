using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Relations.Domain.DTO.UserDto;
using Relations.Domain.Identity.DTO.Auth;
using Relations.Domain.Identity.DTO.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Relations.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenSettings _jwtTokenSettings;
        private readonly IHttpContextAccessor _httpAccessor;

        public UserService(UserManager<ApplicationUser> userManager,
        IOptions<JwtTokenSettings> jwtTokenSettings,
        IHttpContextAccessor httpAccessor)
        {
            _userManager = userManager;
            _jwtTokenSettings = jwtTokenSettings.Value;
            _httpAccessor = httpAccessor;
        }

        public Task<JwtDto> GenerateJwt(ApplicationUser user)
        {
            var expires = DateTime.UtcNow.AddDays(_jwtTokenSettings.JwtExpires);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = _userManager.GetRolesAsync(user).Result;

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.JwtKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtTokenSettings.JwtIssuer,
                audience: _jwtTokenSettings.JwtAudience,
                signingCredentials: creds,
                expires: expires,
                claims: authClaims
            );

            var jwt = new JwtDto
            {
                Token = token,
                Expiration = expires
            };

            return Task.FromResult(jwt);
        }

        public async Task<string> GetUserId()
        {
            var email = GetUserEmail();
            var user = await _userManager.FindByEmailAsync(email);

            return user!.Id;
        }

        private string GetUserEmail()
        {
            var email = _httpAccessor
                .HttpContext?.User.Claims
                .Single(x => x.Type == ClaimTypes.Email).Value;

            return email!;
        }
    }
}
