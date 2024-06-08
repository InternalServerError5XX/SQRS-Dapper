using Microsoft.AspNetCore.Identity;
using Relations.Domain.DTO.CommentDto;
using Relations.Domain.DTO.UserDto;
using Relations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Domain.DTO.PostDto
{
    public class ResponsePostDto
    {
        public long Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public PostUserDto User { get; set; } = null!;
        public List<ResponseCommentDto> Comments { get; set; } = [];
    }
}
