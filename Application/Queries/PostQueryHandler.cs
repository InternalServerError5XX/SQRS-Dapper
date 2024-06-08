using MediatR;
using Relations.Application.Services.PostService;
using Relations.Domain.DTO.PostDto;
using Relations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Application.Queries
{
    public record GetPostsQuery() : IRequest<IQueryable<ResponsePostDto>>;
    public record GetPostQuery(long Id) : IRequest<ResponsePostDto>;

    public class GetPostsQueryHandler(IPostService postService)
        : IRequestHandler<GetPostsQuery, IQueryable<ResponsePostDto>>
    {
        public async Task<IQueryable<ResponsePostDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            return await postService.GetAllDapper();
        }
    }

    public class GetPostQueryHandler(IPostService postService)
        : IRequestHandler<GetPostQuery, ResponsePostDto>
    {
        public async Task<ResponsePostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            return await postService.GetByIdDapper(request.Id);
        }
    }
}
