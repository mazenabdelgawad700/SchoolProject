using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Query.Models
{
    public class ValidateRefreshTokenQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
    }
}
