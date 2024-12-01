using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Responses;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Core.Wrappers;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Hanlders
{
    internal class ApplicationUserQueryHandler : ResponseHandler,
        IRequestHandler<GetPaginatedUserListQuery, PaginatedResult<GetPaginatedUserListResponse>>,
        IRequestHandler<GetApplicationUserByIdQuery, Response<GetApplicationUserByIdResponse>>
    {

        #region Fields
        private readonly IStringLocalizer _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ApplicationUserQueryHandler(IStringLocalizer<SharedResourcesLocalization> localizer
            , UserManager<User> userManager, IMapper mapper
            ) : base(localizer)
        {
            _stringLocalizer = localizer;
            _userManager = userManager;
            _mapper = mapper;
        }

        #endregion

        #region Handlers
        public async Task<PaginatedResult<GetPaginatedUserListResponse>> Handle(GetPaginatedUserListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            if (users is not null)
            {
                var paginatedResult = await _mapper.ProjectTo<GetPaginatedUserListResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return paginatedResult;
            }

            // Temporarly solution
            return null;
        }

        public async Task<Response<GetApplicationUserByIdResponse>> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
        {
            // Notice -> it asks for id of type string ?!
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user is null)
                return BadRequest<GetApplicationUserByIdResponse>($"No user with id: {request.Id}");

            var mappedUser = _mapper.Map<GetApplicationUserByIdResponse>(user);
            return Success<GetApplicationUserByIdResponse>(mappedUser);
        }
        #endregion
    }
}
