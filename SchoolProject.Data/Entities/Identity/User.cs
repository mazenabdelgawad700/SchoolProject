using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {

        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
