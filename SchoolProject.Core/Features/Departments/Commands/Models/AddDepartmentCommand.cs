using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Departments.Commands.Models
{
    public class AddDepartmentCommand : IRequest<Response<string>>
    {
        public string DepartmentName { get; set; }
        public int? DepartmentManagerId { get; set; }
    }
}
