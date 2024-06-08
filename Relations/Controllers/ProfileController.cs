using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Relations.Application.Queries;
using Relations.Application.Services.ProfileService;
using Relations.Domain.DTO.ProfileDto;

namespace RelationsWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController(IProfileService profileService, IMapper mapper, IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseProfileDto>>> GetAll()
        {
            try
            {
                var profiles = await profileService.GetAll();
                var response = mapper.Map<IEnumerable<ResponseProfileDto>>(profiles);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseProfileDto>> Get(long id)
        {
            try
            {
                var profile = await profileService.GetById(id);
                if (profile == null) return NotFound("Couldn`t find the profile");

                var response = mapper.Map<ResponseProfileDto>(profile);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dapper")]
        public async Task<ActionResult<IEnumerable<ResponseProfileDto>>> GetAllDapper()
        {
            try
            {
                var profiles = await profileService.GetAllDapper();
                var response = mapper.Map<IEnumerable<ResponseProfileDto>>(profiles);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("dapper/{id}")]
        public async Task<ActionResult<ResponseProfileDto>> GetDapper(long id)
        {
            try
            {
                var profile = await profileService.GetByIdDapper(id);
                if (profile == null) return NotFound("Couldn`t find the profile");

                var response = mapper.Map<ResponseProfileDto>(profile);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("mediator")]
        public async Task<ActionResult<IEnumerable<ResponseProfileDto>>> GetAllDapperMediatr()
        {
            try
            {
                var profiles = await mediator.Send(new GetProfilesQuery());
                var response = mapper.Map<IEnumerable<ResponseProfileDto>>(profiles);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("mediator/{id}")]
        public async Task<ActionResult<ResponseProfileDto>> GetDapperMediatr(long id)
        {
            try
            {
                var profile = await mediator.Send(new GetProfileQuery(id));
                if (profile == null) return NotFound("Couldn`t find the profile");

                var response = mapper.Map<ResponseProfileDto>(profile);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseProfileDto>> Create([FromQuery] RequestProfileDto profileDto)
        {
            try
            {
                var profile = await profileService.Create(profileDto);
                if (profile == null) return NotFound("Couldn`t create the profile");

                var response = mapper.Map<ResponseProfileDto>(profile);

                return CreatedAtAction(nameof(Get), new { id = profile.Id }, mapper.Map<ResponseProfileDto>(profile));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<ActionResult<ResponseProfileDto>> Create([FromQuery] long id, 
            [FromQuery] RequestProfileDto profileDto)
        {
            try
            {
                var profile = await profileService.Update(id, profileDto);
                if (profile == null) return NotFound("Couldn`t create the profile");

                var response = mapper.Map<ResponseProfileDto>(profile);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
