using AutoMapper;

namespace SchoolProject.Core.Mapping.RolesMapping
{
    public partial class RolesProfile : Profile
    {
        public RolesProfile()
        {
            GetRolesListMapping();
            GetRoleByIdMapping();
        }
    }
}
