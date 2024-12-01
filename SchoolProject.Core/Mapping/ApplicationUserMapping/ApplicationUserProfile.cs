using AutoMapper;

namespace SchoolProject.Core.Mapping.ApplicationUserMapping
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            AddUserMapping();
            GetPaginatedUserListMapping();
            GetApplicationUserByIdMapping();
        }
    }
}
