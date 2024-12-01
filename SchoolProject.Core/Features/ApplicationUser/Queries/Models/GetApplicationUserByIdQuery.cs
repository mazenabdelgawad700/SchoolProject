using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models
{
    public class GetApplicationUserByIdQuery : IRequest<Response<GetApplicationUserByIdResponse>>
    {
        public int Id { get; set; }
    }
}
