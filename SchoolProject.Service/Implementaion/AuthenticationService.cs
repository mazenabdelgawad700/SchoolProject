using Microsoft.IdentityModel.Tokens;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Service.Implementaion
{
    public class AuthenticationService : IAuthenticationService
    {

        #region Fields
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        #endregion

        #region Handlers
        public async Task<string> GetJWTToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
            };

            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new
                SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                        SecurityAlgorithms.HmacSha512Signature
                )
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return await Task.FromResult(accessToken);
        }
        #endregion

    }
}
