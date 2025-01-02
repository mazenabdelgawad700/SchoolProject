using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using SchoolProject.Domain.Requests;
using SchoolProject.Domain.Results;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Service.Abstracts;
using System.Security.Claims;

namespace SchoolProject.Service.Implementaion
{
    public class AuthorizationService : IAuthorizationService
    {

        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dpContext;
        #endregion

        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, AppDbContext dpContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dpContext = dpContext;
        }

        #endregion

        #region Handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            try
            {
                var identityRole = new Role();
                identityRole.Name = roleName;
                var role = await _roleManager.CreateAsync(identityRole);

                if (role.Succeeded)
                    return "Success";

                return "Failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Failed";
            }
        }
        public async Task<string> DeleteRoleAsync(int roleId)
        {
            try
            {
                var roleToDelete = await _roleManager.Roles
                    .Where(x => x.Id == roleId).FirstOrDefaultAsync();

                if (roleToDelete is null)
                    return "Failed";

                var role = await _roleManager.DeleteAsync(roleToDelete);

                if (role.Succeeded)
                    return "Success";

                return "Failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Failed";
            }
        }
        public async Task<string> EditRoleAsync(string roleName, int roleId)
        {
            try
            {
                var roleToUpdate = await _roleManager.Roles
                    .Where(x => x.Id == roleId).FirstOrDefaultAsync();

                if (roleToUpdate is null)
                    return "Failed";

                roleToUpdate.Name = roleName;

                var role = await _roleManager.UpdateAsync(roleToUpdate);

                if (role.Succeeded)
                    return "Success";

                return "Failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Failed";
            }
        }
        public async Task<ManageUserRolesResponse> ManageUserRolesData(User user)
        {
            try
            {
                ManageUserRolesResponse response = new ManageUserRolesResponse();
                List<UserRoles> rolesList = [];
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                List<Role> roles = await _roleManager.Roles.ToListAsync();

                response.UserId = user.Id;

                foreach (Role role in roles)
                {
                    UserRoles currentUserRole = new()
                    {
                        Id = role.Id,
                        Name = role.Name!
                    };
                    if (userRoles.Contains(role.Name))
                    {
                        currentUserRole.HasRole = true;
                    }
                    else
                    {
                        currentUserRole.HasRole = false;
                    }
                    rolesList.Add(currentUserRole);
                }
                response.UserRoles = rolesList;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }


        }
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var role = await _roleManager.Roles.Where(x => x.Id == roleId).FirstOrDefaultAsync();
                return role!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();

                if (roles is not null)
                    return roles;

                return new List<Role>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Role>();
            }
        }
        public async Task<bool> IsRoleExistAsync(string roleName)
        {
            try
            {
                return await _roleManager.RoleExistsAsync(roleName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> IsRoleHaveUsers(int roleId)
        {
            try
            {
                var role = await _roleManager.Roles.Where(x => x.Id == roleId).FirstOrDefaultAsync();

                if (role is null)
                    return false;

                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                return usersInRole.Any();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> IsRoleNameUsedButNotTheSameRuleAsync(string roleName, int roleId)
        {
            try
            {
                var role = await _roleManager.Roles
                    .Where(x => x.Name == roleName && x.Id != roleId).FirstOrDefaultAsync();

                return role is not null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            IDbContextTransaction transaction = await _dpContext.Database.BeginTransactionAsync();
            try
            {

                User user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user is null)
                    return "UserIsNull";

                IList<string> oldUserRoles = await _userManager.GetRolesAsync(user);

                IdentityResult removeRolesResult = await _userManager.RemoveFromRolesAsync(user, oldUserRoles);

                if (!removeRolesResult.Succeeded)
                    return "FailedToRemoveRoles";

                List<string> selectedRules = request.UserRoles
                    .Where(x => x.HasRole == true).Select(x => x.Name).ToList();

                IdentityResult addNewRolesResult = await _userManager.AddToRolesAsync(user, selectedRules);


                if (!addNewRolesResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return "FailedToAddNewRoles";
                }

                await transaction.CommitAsync();
                return "Success";
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return "Error";
            }
        }
        public async Task<ManageUserClaimsResults> ManageUserClaimsDataAsync(User user)
        {
            try
            {
                ManageUserClaimsResults response = new();
                response.UserId = user.Id;

                List<UserClaim> userClaimsList = [];

                IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);

                foreach (Claim claim in ClaimStore.Claims)
                {
                    UserClaim userClaim = new()
                    {
                        Type = claim.Type
                    };
                    if (userClaims.Any(x => x.Type == claim.Type))
                    {
                        userClaim.Value = true;
                    }
                    else
                    {
                        userClaim.Value = false;
                    }
                    userClaimsList.Add(userClaim);
                }
                response.UserClaims = userClaimsList;

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
        #endregion
    }
}
