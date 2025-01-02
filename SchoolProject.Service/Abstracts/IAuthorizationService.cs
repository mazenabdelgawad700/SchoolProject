using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Requests;
using SchoolProject.Domain.Results;

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
        public Task<ManageUserRolesResponse> ManageUserRolesData(User user);
        public Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request);
        public Task<ManageUserClaimsResults> ManageUserClaimsDataAsync(User user);
    }
}
