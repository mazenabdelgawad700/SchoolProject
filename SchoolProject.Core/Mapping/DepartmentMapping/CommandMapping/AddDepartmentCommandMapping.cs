using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void AddDepartmentMapping()
        {
            CreateMap<AddDepartmentCommand, Department>()
                .ForMember(dest => dest.DName, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.InsManager, opt => opt.MapFrom(src => src.DepartmentManagerId));
        }
    }
}
