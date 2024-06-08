using Microsoft.AspNetCore.Identity;
using Relations.Domain.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.Entity
{
    public class Post : BaseModel
    {
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date {  get; set; }

        public ApplicationUser User { get; set; } = null!;
        public List<Comment> Comments { get; set; } = [];
    }
}
