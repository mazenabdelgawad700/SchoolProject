using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Domain.Results;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResults>>
    {
        public int UserId { get; set; }
    }
}
