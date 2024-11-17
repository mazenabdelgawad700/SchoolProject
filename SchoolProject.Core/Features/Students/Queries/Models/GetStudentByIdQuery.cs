using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Responses;

namespace SchoolProject.Core.Features.Students.Queries.Models;

public class GetStudentByIdQuery: IRequest<Response<GetSingleStudentResponse>>
{
    public GetStudentByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}
