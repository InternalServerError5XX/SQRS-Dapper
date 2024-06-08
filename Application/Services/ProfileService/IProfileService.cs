using Relations.Application.Services.BaseService;
using Relations.Domain.DTO.PostDto;
using Relations.Domain.DTO.ProfileDto;
using Relations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Application.Services.ProfileService
{
    public interface IProfileService : IBaseService<UserProfile>
    {
        Task<IEnumerable<UserProfile>> GetAll();
        Task<UserProfile?> GetById(long id);
        Task<IQueryable<UserProfile>> GetAllDapper();
        Task<UserProfile?> GetByIdDapper(long id);
        Task<UserProfile> Create(RequestProfileDto profileDto);
        Task<UserProfile> Update(long id, RequestProfileDto profileDto);
    }
}
