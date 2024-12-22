using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Enums;
using SchoolProject.Infrastructure.Exceptions;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount <= 0)
            {
                try
                {
                    var adminRole = new Role() { Name = nameof(RolesEnum.Admin) };
                    var addAdminRoleResult = await _roleManager.CreateAsync(adminRole);

                    if (!addAdminRoleResult.Succeeded)
                        throw new FailedToAddDefaultRoleException($"Failed to add default {nameof(RolesEnum.Admin)} role", 500);

                    var userRole = new Role() { Name = nameof(RolesEnum.User) };
                    var addUserRoleResult = await _roleManager.CreateAsync(userRole);

                    if (!addUserRoleResult.Succeeded)
                        throw new FailedToAddDefaultRoleException($"Failed to add default {nameof(RolesEnum.User)} role", 500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
