using Microsoft.IdentityModel.Tokens;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using SchoolProject.Service.Abstracts;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementaion
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, ConcurrentDictionary<string, RefreshToken> userRefreshToken)
        {
            _jwtSettings = jwtSettings;
            _userRefreshToken = userRefreshToken;
        }
        #endregion

        #region Handlers
        public JwtAuthResponse GetJWTToken(User user)
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
                expires: DateTime.UtcNow.AddMonths(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new
                SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                        SecurityAlgorithms.HmacSha512Signature
                )
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.UtcNow.AddMonths(_jwtSettings.RefreshTokenExpireDate),
                UserName = user.UserName,
                TokenString = GenerateRefreshToken()
            };

            _userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);

            return new JwtAuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
        #endregion
    }
}
