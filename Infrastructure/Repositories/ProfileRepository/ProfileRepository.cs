using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Infrastructure.Repositories.ProfileRepository
{
    public class ProfileRepository : BaseRepository<UserProfile>, IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext context, IDbConnection dbConnection) : base(context, dbConnection)
        {
        }
    }
}
