using SchoolProject.Core.Features.Authorization.Queries.Responeses;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Core.Mapping.RolesMapping
{
    public partial class RolesProfile
    {
        void GetRoleByIdMapping()
        {
            CreateMap<Role, GetRoleByIdResponse>();
        }
    }
}
