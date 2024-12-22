using SchoolProject.Domain.DTOs;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistAsync(string roleName);
        public Task<bool> IsRoleNameUsedButNotTheSameRuleAsync(string roleName, int roleId);
        public Task<string> EditRoleAsync(string roleName, int roleId);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<bool> IsRoleHaveUsers(int roleId);
        public Task<List<Role>> GetRolesAsync();
        public Task<Role> GetRoleByIdAsync(int roleId);
        public Task<ManageUserRolesResponse> GetManageUserRolesData(User user);
        public Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request);
    }
}
