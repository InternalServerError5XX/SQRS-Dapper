using Microsoft.AspNetCore.Identity;
using Relations.Domain.DTO.UserDto;
using Relations.Domain.Identity.DTO;
using Relations.Domain.Identity.DTO.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Application.Services.UserService
{
    public interface IUserService
    {
        Task<JwtDto> GenerateJwt(ApplicationUser user);
        Task<string> GetUserId();
    }
}
