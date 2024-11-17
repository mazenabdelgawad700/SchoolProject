using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Responses;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }
        public int StudentPageSize { get; set; } = 10;
        public int StudentPageNumber { get; set; } = 1;
    }
}
