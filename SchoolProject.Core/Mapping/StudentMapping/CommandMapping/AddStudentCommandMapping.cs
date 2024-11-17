using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping;

public partial class StudentProfile
{
    public void AddStudentMapping()
    {
        CreateMap<AddStudentCommand, Student>()
            .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartementId));
    }

}
