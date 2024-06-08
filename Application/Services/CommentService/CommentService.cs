using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Relations.Application.Services.UserService;
using Relations.Domain.DTO.CommentDto;
using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.CommentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Application.Services.CommentService
{
    public class CommentService(ICommentRepository commentRepository, IMapper mapper,
        IUserService userService) : ICommentService
    {
        public async Task<Comment> Add(RequestCommentDto commentDto)
        {
            var comment = mapper.Map<Comment>(commentDto);
            comment.UserId = await userService.GetUserId();

            var response = await commentRepository.Create(comment);
            return response;
        }

        public async Task<IEnumerable<Comment>> GetAll(long postId)
        {
            var comments = await GetComments();
            return comments.Where(x => x.PostId == postId);
        }

        public async Task<Comment?> GetById(long id)
        {
            var comments = await GetComments();
            return await comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<IQueryable<Comment>> GetComments()
        {
            var comments = await commentRepository.GetAll();
            return comments.Include(x => x.User);
        }
    }
}
