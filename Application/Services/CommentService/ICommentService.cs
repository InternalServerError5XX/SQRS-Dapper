using Relations.Application.Services.BaseService;
using Relations.Domain.DTO.CommentDto;
using Relations.Domain.Entity;

namespace Relations.Application.Services.CommentService
{
    public interface ICommentService : IBaseService<Comment>
    {
        Task<IEnumerable<Comment>> GetAll(long postId);
        Task<Comment?> GetById(long Id);
        Task<Comment> Add(RequestCommentDto commentDto);
    }
}
