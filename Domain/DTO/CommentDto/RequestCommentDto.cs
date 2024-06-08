using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.DTO.CommentDto
{
    public class RequestCommentDto
    {
        public long PostId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
