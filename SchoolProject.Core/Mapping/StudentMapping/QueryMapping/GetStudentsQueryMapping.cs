using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping;

public partial class StudentProfile
{
    public void GetStudentsMapping()
    {
        CreateMap<Student, GetStudentsResponse>()
           .ForMember(dest => dest.DepartementName,
                      opt => opt.MapFrom(
                          src => src.Department!.DName
                      )
           );
    }
}