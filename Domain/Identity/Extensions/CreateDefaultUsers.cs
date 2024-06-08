using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Relations.Domain.DTO.UserDto;
using Relations.Domain.Identity.Static;

namespace Relations.Domain.Identity.Extensions
{
    public static class CreateDefaultUsersExtension
    {
        public static async Task CreateDefaultUsers(this WebApplication app) 
        {
            var scope = app.Services.CreateAsyncScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByEmailAsync(DefaultUsersCredentials.AdminEmail) is null)
            {
                var user = new ApplicationUser
                {
                    Email = DefaultUsersCredentials.AdminEmail,
                    UserName = DefaultUsersCredentials.AdminName,
                };

                await userManager.CreateAsync(user, DefaultUsersCredentials.AdminPassword);
                await userManager.AddToRoleAsync(user, DefaultRoles.Admin);
            }             
        }
    }
}
