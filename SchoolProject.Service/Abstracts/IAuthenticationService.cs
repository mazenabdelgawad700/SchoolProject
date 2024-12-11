using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public JwtAuthResponse GetJWTToken(User user);
    }
}
