using Dapper;
using Microsoft.EntityFrameworkCore;
using Relations.Domain.DTO.CommentDto;
using Relations.Domain.DTO.PostDto;
using Relations.Domain.DTO.ProfileDto;
using Relations.Domain.DTO.UserDto;
using Relations.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Relations.Infrastructure.Repositories.PostRepository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        private readonly IDbConnection _dbConnection;

        public PostRepository(ApplicationDbContext context, IDbConnection dbConnection) : base(context, dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IQueryable<ResponsePostDto>> GetPostsDapper()
        {
            var query = @"
                SELECT p.*, u.*, up.*, c.*, cu.*, cup.*
                FROM Posts p
                    JOIN AspNetUsers u ON p.UserId = u.Id
                    JOIN UserProfiles up ON u.ProfileId = up.Id
                    LEFT JOIN Comments c ON p.Id = c.PostId
                    LEFT JOIN AspNetUsers cu ON c.UserId = cu.Id
                    LEFT JOIN UserProfiles cup ON cu.ProfileId = cup.Id";

            var postDictionary = new Dictionary<long, ResponsePostDto>();

            var result = await _dbConnection.QueryAsync<ResponsePostDto, PostUserDto, PostProfileDto,
                ResponseCommentDto, PostUserDto, PostProfileDto, ResponsePostDto>(query,
                (post, user, userProfile, comment, commentUser, commentUserProfile) =>
                {
                    if (!postDictionary.TryGetValue(post.Id, out var currentPost))
                    {
                        currentPost = post;
                        currentPost.User = user;
                        currentPost.User.Profile = userProfile;
                        currentPost.Comments = new List<ResponseCommentDto>();
                        postDictionary.Add(currentPost.Id, currentPost);
                    }

                    if (comment != null)
                    {
                        comment.User = commentUser;
                        comment.User.Profile = commentUserProfile;
                        currentPost.Comments.Add(comment);
                    }

                    return currentPost;
                }
            );

            return postDictionary.Values.ToList().AsQueryable();
        }
    }
}
