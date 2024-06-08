using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Infrastructure.Repositories.ProfileRepository
{
    public interface IProfileRepository : IBaseRepository<UserProfile>;
}
