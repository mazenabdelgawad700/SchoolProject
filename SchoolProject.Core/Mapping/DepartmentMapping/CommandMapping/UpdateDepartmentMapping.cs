using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void UpdateDepartmentMapping()
        {
            CreateMap<UpdateDepartmentCommand, Department>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DName, opt => opt.MapFrom(src => src.DepartmentName))
            .ForMember(dest => dest.InsManager, opt => opt.MapFrom(src => src.DepartmentManagerId));
        }
    }
}
