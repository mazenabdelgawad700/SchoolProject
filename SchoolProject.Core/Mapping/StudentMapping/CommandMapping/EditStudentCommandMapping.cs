using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping;

public partial class StudentProfile
{
    public void EditStudentMapping()
    {
        CreateMap<EditStudentCommand, Student>()
            .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartementId))
            .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id));
    }
}
