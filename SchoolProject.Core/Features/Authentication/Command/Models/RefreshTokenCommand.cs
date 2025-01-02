using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Domain.Results;

namespace SchoolProject.Core.Features.Authentication.Command.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
