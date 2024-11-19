using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Departments.Commands.Models
{
    public class UpdateDepartmentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentManagerId { get; set; }
    }
}
