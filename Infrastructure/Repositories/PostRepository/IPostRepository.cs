using Relations.Domain.DTO.PostDto;
using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.BaseRepository;

namespace Relations.Infrastructure.Repositories.PostRepository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IQueryable<ResponsePostDto>> GetPostsDapper();
    }
}
