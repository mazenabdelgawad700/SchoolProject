using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void DeleteDepartmentMapping()
        {
            CreateMap<DeleteDepartmentCommand, Department>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.Id));
        }
    }
}
