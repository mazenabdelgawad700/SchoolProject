using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimCommandHandler : ResponseHandler,
        IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public ClaimCommandHandler(IStringLocalizer<SharedResourcesLocalization> localizer, IAuthorizationService authorizationService)
            : base(localizer)
        {
            _authorizationService = authorizationService;
            _localizer = localizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string updateUserClaimsResult = await _authorizationService.UpdateUserClaimsAsync(request);
                switch (updateUserClaimsResult)
                {
                    case "InvalidUserId":
                        return NotFound<string>(
                            _localizer[LocalizationSharedResourcesKeys.InvalidUserId]);
                    case "FailedToGetClaims":
                        return Success<string>(_localizer[LocalizationSharedResourcesKeys.FailedToGetClaims]);
                    case "FailedToRemoveClaims":
                        return Success<string>(_localizer[LocalizationSharedResourcesKeys.FailedToRemoveClaims]);
                    case "FailedToSelectClaims":
                        return Success<string>(_localizer[LocalizationSharedResourcesKeys.FailedToSelectClaims]);
                    case "FailedToUpdateClaims":
                        return Success<string>(_localizer[LocalizationSharedResourcesKeys.FailedToUpdateClaims]);
                    case "Success":
                        return Success<string>(_localizer[LocalizationSharedResourcesKeys.UpdateClaimsSuccessfully]);
                    case "Error":
                        return Success<string>(_localizer[LocalizationSharedResourcesKeys.ErrorDuringUpdatingClaims]);
                    default:
                        return Failed<string>();
                }
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
