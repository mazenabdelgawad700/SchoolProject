using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Domain.Results;

namespace SchoolProject.Core.Features.Authentication.Command.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
