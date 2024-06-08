using AutoMapper;
using Relations.Application.Services.UserService;
using Relations.Domain.DTO.ProfileDto;
using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.CommentRepository;
using Relations.Infrastructure.Repositories.ProfileRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Application.Services.ProfileService
{
    public class ProfileService(IProfileRepository profileRepository, 
        IMapper mapper, IUserService userService) : IProfileService
    {
        public async Task<UserProfile> Create(RequestProfileDto profileDto)
        {
            var profile = mapper.Map<UserProfile>(profileDto);
            profile.UserId = await userService.GetUserId();

            var response = await profileRepository.Create(profile);
            return response;
        }

        public async Task<IEnumerable<UserProfile>> GetAll()
        {
            var profiles = await profileRepository.GetAll();

            return profiles;
        }       

        public async Task<UserProfile?> GetById(long id)
        {
            var profile = await profileRepository.GetById(id);

            return profile;
        }

        public async Task<IQueryable<UserProfile>> GetAllDapper()
        {
            var profiles = await profileRepository.GetAllDapper();

            return profiles;
        }

        public async Task<UserProfile?> GetByIdDapper(long id)
        {
            var profile = await profileRepository.GetByIdDapper(id);

            return profile;
        }

        public async Task<UserProfile> Update(long id, RequestProfileDto profileDto)
        {
            var existingProfile = await GetById(id);
            if (existingProfile == null)
                throw new NullReferenceException("There is no profile with such id");

            var profile = mapper.Map<UserProfile>(profileDto);
            profile.Id = existingProfile.Id;
            profile.UserId = existingProfile.UserId;

            var response = await profileRepository.Update(x => x.Id == id, profile);
            return response;
        }
    }
}
