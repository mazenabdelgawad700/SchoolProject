using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models
{
    public class DeleteApplicationUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteApplicationUserCommand(int id)
        {
            Id = id;
        }
    }
}
