using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Relations.Application.Queries;
using Relations.Application.Services.CommentService;
using Relations.Application.Services.PostService;
using Relations.Application.Services.ProfileService;
using Relations.Application.Services.UserService;
using Relations.Infrastructure.Repositories.CommentRepository;
using Relations.Infrastructure.Repositories.PostRepository;
using Relations.Infrastructure.Repositories.ProfileRepository;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace Relations
{
    public static class Initializer
    {
        public static async Task InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            await Task.CompletedTask;
        }

        public static async Task InitializeServices(this IServiceCollection services)
        {           
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IProfileService, ProfileService>();

            await Task.CompletedTask;
        }

        public static async Task InitializeSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization,
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            await Task.CompletedTask;
        }

        public static async Task InitializeMediator(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf
                .RegisterServicesFromAssembly(typeof(GetProfilesQueryHandler).Assembly));
            services.AddMediatR(cf => cf
                .RegisterServicesFromAssembly(typeof(GetProfileQueryHandler).Assembly));
            services.AddMediatR(cf => cf
                .RegisterServicesFromAssembly(typeof(GetPostsQueryHandler).Assembly));
            services.AddMediatR(cf => cf
                .RegisterServicesFromAssembly(typeof(GetPostQueryHandler).Assembly));

            await Task.CompletedTask;
        }
    }
}
