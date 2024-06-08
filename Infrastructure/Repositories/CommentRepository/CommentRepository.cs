using Relations.Domain.Entity;
using Relations.Infrastructure.Repositories.PostRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relations.Infrastructure.Repositories.CommentRepository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context, IDbConnection dbConnection) : base(context, dbConnection)
        {
        }
    }
}
