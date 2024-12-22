using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Responeses;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Domain.DTOs;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
        IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResponse>>>,
        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
        IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResponse>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public RoleQueryHandler(IStringLocalizer<SharedResourcesLocalization> localizer, IAuthorizationService authorizationService, IMapper mapper, UserManager<User> userManager) : base(localizer)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Handlers
        public async Task<Response<List<GetRolesListResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getRolesListResult = await _authorizationService.GetRolesAsync();
                var mappedResult = _mapper.Map<List<GetRolesListResponse>>(getRolesListResult);
                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Failed<List<GetRolesListResponse>>();
            }
        }
        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _authorizationService.GetRoleByIdAsync(request.Id);
                if (role is null)
                    return NotFound<GetRoleByIdResponse>();
                var mappedResult = _mapper.Map<GetRoleByIdResponse>(role);
                return Success(mappedResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Failed<GetRoleByIdResponse>();
            }
        }
        public async Task<Response<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId <= 0)
                    return BadRequest<ManageUserRolesResponse>
                        (_localizer[LocalizationSharedResourcesKeys.InvalidUserId]);

                User user = await _userManager.FindByIdAsync(request.UserId.ToString());

                if (user is null)
                    return NotFound<ManageUserRolesResponse>
                        (_localizer[LocalizationSharedResourcesKeys.UserNotFound]);

                ManageUserRolesResponse response = await
                    _authorizationService.GetManageUserRolesData(user);

                if (response is null)
                    return Failed<ManageUserRolesResponse>();

                return Success(response);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Failed<ManageUserRolesResponse>();
            }
        }
        #endregion
    }
}
