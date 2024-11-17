using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping;

public partial class StudentProfile
{
    public void DeleteStudentMapping()
    {
        CreateMap<DeleteStudentCommand, Student>()
                .ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.StudID));
    }

}
