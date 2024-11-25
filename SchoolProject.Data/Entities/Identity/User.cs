using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchoolProject.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
    }
}
