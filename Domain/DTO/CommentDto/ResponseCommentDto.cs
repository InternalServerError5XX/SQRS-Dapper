using Relations.Domain.DTO.UserDto;
using Relations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.DTO.CommentDto
{
    public class ResponseCommentDto
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public PostUserDto User { get; set; } = null!;
    }
}
