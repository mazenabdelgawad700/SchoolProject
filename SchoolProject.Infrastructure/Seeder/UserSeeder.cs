using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Enums;
using SchoolProject.Infrastructure.Exceptions;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                try
                {
                    var defaultUser = new User()
                    {
                        FullName = "mazen abdelgawad",
                        Email = "admin@school.com",
                        UserName = "mazen",
                        Country = "Egypt",
                        PhoneNumber = "01158907731",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Address = "Cairo, 15 May City"
                    };
                    var addFirstUserResult = await _userManager
                        .CreateAsync(defaultUser, "mazenM123@");
                    if (!addFirstUserResult.Succeeded)
                        throw new FailedToAddUserException("Failed To Add User", 500);

                    var addUserToRoleResult = await _userManager.AddToRoleAsync(defaultUser,
                        nameof(RolesEnum.Admin));

                    if (!addUserToRoleResult.Succeeded)
                        throw new FailedToAddUserToRoleException("Failed To Add User To Role", 500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
