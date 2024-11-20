using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Domain.Identity
{
    public class User : IdentityUser
    {
        public string Address { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
