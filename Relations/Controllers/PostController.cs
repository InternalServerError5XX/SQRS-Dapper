using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Relations.Application.Queries;
using Relations.Application.Services.PostService;
using Relations.Domain.DTO.PostDto;
using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.PostRepository;

namespace RelationsWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController(IPostService postService, IPostRepository postRepository,
        IMapper mapper, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponsePostDto>>> GetAll()
        {
            try
            {
                var posts = await postService.GetAll();
                var response = mapper.Map<IEnumerable<ResponsePostDto>>(posts);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestPostDto>> Get([FromRoute] long id)
        {
            try
            {
                var post = await postService.GetById(id);
                if (post == null) return BadRequest("Couldn`t find the post");

                var response = mapper.Map<ResponsePostDto>(post);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dapper")]
        public async Task<ActionResult<IEnumerable<ResponsePostDto>>> GetAllDapper()
        {
            try
            {
                var posts = await postService.GetAllDapper();

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dapper/{id}")]
        public async Task<ActionResult<RequestPostDto>> GetDapper([FromRoute] long id)
        {
            try
            {
                var post = await postService.GetByIdDapper(id);
                if (post == null) return BadRequest("Couldn`t find the post");

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("mediator")]
        public async Task<ActionResult<IEnumerable<ResponsePostDto>>> GetAllMediator()
        {
            try
            {
                var posts = await mediator.Send(new GetPostsQuery());

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("mediator/{id}")]
        public async Task<ActionResult<RequestPostDto>> GetMediator([FromRoute] long id)
        {
            try
            {
                var post = await mediator.Send(new GetPostQuery(id));
                if (post == null) return BadRequest("Couldn`t find the post");

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponsePostDto>> Create([FromQuery] RequestPostDto postDto)
        {
            try
            {
                var post = await postService.Create(postDto);
                if (post == null) return BadRequest("Couldn't create the post");

                return CreatedAtAction(nameof(Get), new { id = post.Id }, mapper.Map<ResponsePostDto>(post));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            try
            {
                var posts = await postRepository.GetById(id);              
                if (posts == null) return BadRequest("Couldn`t find the post");

                var response = await postRepository.Delete(x => x.Id == id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<ResponsePostDto>> Update([FromQuery] long id, 
            [FromQuery] RequestPostDto postDto)
        {
            try
            {
                var post = await postService.Update(id, postDto);
                if (post == null) return BadRequest("Couldn't create the post");

                var response = mapper.Map<ResponsePostDto>(post);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
