using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResponse> GetJWTTokenAsync(User user);
        public JwtSecurityToken ReadJWTTokenAsync(string accessToken);
        public Task<(string, DateTime?)> ValidateDetailsAsync(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        public Task<JwtAuthResponse> GetRefreshTokenAsync(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateTokenAsync(string AccessToken);
    }
}
