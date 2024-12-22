using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public RoleCommandHandler(IStringLocalizer<SharedResourcesLocalization> localizer,
            IAuthorizationService authorizationService
            ) : base(localizer)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var createRoleResult = await _authorizationService.AddRoleAsync(request.RoleName);
            if (createRoleResult == "Success") return Created(createRoleResult);
            else return Failed<string>();
        }
        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var updateRoleResult = await _authorizationService.EditRoleAsync(request.Name, request.Id);
            if (updateRoleResult == "Success")
                return Updated<string>();

            else return Failed<string>();
        }
        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var deleteRoleResult = await _authorizationService.DeleteRoleAsync(request.Id);
            if (deleteRoleResult == "Success")
                return Deleted<string>();
            return Failed<string>();
        }
        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string updateUserRolesResult = await _authorizationService.UpdateUserRolesAsync(request);

                if (updateUserRolesResult == "UserIsNull")
                    return BadRequest<string>(_localizer[LocalizationSharedResourcesKeys.UserNotFound]);

                if (updateUserRolesResult == "FailedToRemoveRoles" || updateUserRolesResult == "FailedToAddNewRoles" || updateUserRolesResult == "Error")
                    return Failed<string>();

                if (updateUserRolesResult == "Success")
                    return Updated<string>();

                return Failed<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Failed<string>();
            }
        }
        #endregion
    }
}
