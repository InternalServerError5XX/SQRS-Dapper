using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Relations.Application.Services.UserService;
using Relations.Domain.DTO.PostDto;
using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.PostRepository;
using static Dapper.SqlMapper;

namespace Relations.Application.Services.PostService
{
    public class PostService(IPostRepository postRepository, IMapper mapper,
        IUserService userService) : IPostService
    {
        public async Task<Post> Create(RequestPostDto postDto)
        {
            var post = mapper.Map<Post>(postDto);
            post.UserId = await userService.GetUserId();
            var response = await postRepository.Create(post);          

            return response;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var posts = await GetPosts();
            return await posts.ToListAsync();
        }

        public async Task<Post?> GetById(long id)
        {
            var posts = await GetPosts();
            return await posts.FirstOrDefaultAsync(x => x.Id == id);
        }      

        public async Task<IQueryable<ResponsePostDto>> GetAllDapper()
        {
            return await postRepository.GetPostsDapper();
        }

        public async Task<ResponsePostDto?> GetByIdDapper(long id)
        {
            return await Task.FromResult(
                postRepository
                .GetPostsDapper().Result
                .FirstOrDefault(x => x.Id == id));
        }

        public async Task<Post> Update(long id, RequestPostDto postDto)
        {
            var existingPost = await GetById(id);
            if (existingPost == null)
                throw new NullReferenceException("There is no post with such id");

            var post = mapper.Map<Post>(postDto);
            post.Id = existingPost!.Id;
            post.UserId = existingPost!.UserId;

            var response = await postRepository.Update(x => x.Id == id, post);
            response.User = existingPost!.User;

            return response;
        }

        private async Task<IQueryable<Post>> GetPosts()
        {
            var posts = await postRepository.GetAll();
            return posts
                .Include(p => p.User)
                    .ThenInclude(u => u.Profile)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                        .ThenInclude(u => u.Profile);
        }       
    }
}
