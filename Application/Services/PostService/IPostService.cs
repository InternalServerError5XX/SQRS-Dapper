using Relations.Application.Services.BaseService;
using Relations.Domain.DTO.PostDto;
using Relations.Domain.Entity;

namespace Relations.Application.Services.PostService
{
    public interface IPostService : IBaseService<Post>
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post?> GetById(long id);
        Task<IQueryable<ResponsePostDto>> GetAllDapper();
        Task<ResponsePostDto?> GetByIdDapper(long id);
        Task<Post> Create(RequestPostDto postDto);
        Task<Post> Update(long id, RequestPostDto postDto);
    }
}
