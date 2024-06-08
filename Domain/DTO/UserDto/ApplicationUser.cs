using Microsoft.AspNetCore.Identity;
using Relations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.DTO.UserDto
{
    public class ApplicationUser : IdentityUser
    {
        public long ProfileId { get; set; }
        public UserProfile Profile { get; set; } = null!;
        public List<Post> Posts { get; set; } = [];
        public List<Comment> Comments { get; set; } = [];
    }
}
