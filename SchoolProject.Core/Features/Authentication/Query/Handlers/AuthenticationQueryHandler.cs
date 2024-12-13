using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Query.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Query.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
        IRequestHandler<ValidateRefreshTokenQuery, Response<string>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructors 
        public AuthenticationQueryHandler(IStringLocalizer<SharedResourcesLocalization> localizer,
            IAuthenticationService authenticationService
            ) : base(localizer)
        {
            _localizer = localizer;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(ValidateRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validateTokenResult = await _authenticationService.ValidateTokenAsync(request.AccessToken);
                if (validateTokenResult == "InvalidToken")
                    return BadRequest<string>("Invalid Token");

                return Success(validateTokenResult);
            }
            catch
            {
                return Failed<string>();
            }
        }
        #endregion
    }
}
