using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping;

public partial class StudentProfile
{
    public void GetStudentByIdMapping()
    {
        CreateMap<Student, GetSingleStudentResponse>()
            .ForMember(dest => dest.DepartementName,
                       opt => opt.MapFrom(src => src.Department!.DName)
            );
    }
}
