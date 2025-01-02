using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Domain.Requests;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesRequest, IRequest<Response<string>>
    {
    }
}
