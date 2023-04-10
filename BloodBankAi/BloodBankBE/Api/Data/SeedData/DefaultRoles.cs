using Api.Const;
using Microsoft.AspNetCore.Identity;

namespace Api.Data.SeedData
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManger)
        {
            if (!roleManger.Roles.Any())
            {
                await roleManger.CreateAsync(new IdentityRole(Roles.Member.ToString()));
                await roleManger.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManger.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
                await roleManger.CreateAsync(new IdentityRole(Roles.BankAdmin.ToString()));
                await roleManger.CreateAsync(new IdentityRole(Roles.BankModerator.ToString()));
            }
        }
    }
}
