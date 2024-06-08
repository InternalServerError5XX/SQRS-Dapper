using Microsoft.AspNetCore.Identity;
using Relations.Domain.DTO.ProfileDto;
using Relations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.DTO.UserDto
{
    public class PostUserDto : IdentityUser
    {
        public PostProfileDto Profile { get; set; } = null!;
    }
}
