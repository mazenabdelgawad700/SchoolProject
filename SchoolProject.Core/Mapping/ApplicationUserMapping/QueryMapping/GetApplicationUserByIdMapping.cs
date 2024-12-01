using AutoMapper;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUserMapping
{
    public partial class ApplicationUserProfile : Profile
    {
        public void GetApplicationUserByIdMapping()
        {
            CreateMap<User, GetApplicationUserByIdResponse>();
        }
    }
}
