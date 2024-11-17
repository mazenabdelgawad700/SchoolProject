using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentsMapping()
        {
            CreateMap<Department, GetDepartmentsResponse>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.DName))
                .ForMember(dest => dest.DepartmentManager, opt => opt.MapFrom(src => src.InsManager));
        }
    }
}
