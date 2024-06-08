using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relations.Application.Services.CommentService;
using Relations.Application.Services.PostService;
using Relations.Domain.DTO.CommentDto;
using Relations.Domain.DTO.PostDto;
using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.CommentRepository;

namespace RelationsWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController(ICommentService commentService, IMapper mapper,
        ICommentRepository commentRepository) : ControllerBase
    {
        [HttpGet("post-comments/{postId}")]
        public async Task<ActionResult<IEnumerable<ResponseCommentDto>>> GetAll([FromRoute] long postId)
        {
            try
            {
                var comments = await commentService.GetAll(postId);

                var response = mapper.Map<IEnumerable<ResponseCommentDto>>(comments);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseCommentDto>> Get([FromRoute] long id)
        {
            try
            {
                var comment = await commentService.GetById(id);
                if (comment == null) return NotFound("Couldn`t find the comment");

                var response = mapper.Map<ResponseCommentDto>(comment);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseCommentDto>> Create([FromQuery] RequestCommentDto commentDto)
        {
            try
            {
                var comment = await commentService.Add(commentDto);
                if (comment == null) return NotFound("Couldn`t add the comment");

                var response = mapper.Map<ResponseCommentDto>(comment);

                return CreatedAtAction(nameof(Get), new { id = comment.Id }, mapper.Map<ResponseCommentDto>(comment));
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
                var comment = await commentService.GetById(id);
                if (comment == null) return BadRequest("Couldn`t find the comment");

                var response = await commentRepository.Delete(x => x.Id == id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
