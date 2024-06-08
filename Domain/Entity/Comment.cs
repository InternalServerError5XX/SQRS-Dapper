using Microsoft.AspNetCore.Identity;
using Relations.Domain.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.Entity
{
    public class Comment : BaseModel
    {
        public long PostId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public Post Post { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
