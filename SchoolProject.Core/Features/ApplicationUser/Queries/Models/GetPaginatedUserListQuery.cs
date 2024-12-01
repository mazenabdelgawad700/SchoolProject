using MediatR;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Models
{
    public class GetPaginatedUserListQuery : IRequest<PaginatedResult<GetPaginatedUserListResponse>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
