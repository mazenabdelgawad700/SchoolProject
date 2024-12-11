using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Helpers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Command.Handlers
{
    internal class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResponse>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IStringLocalizer<SharedResourcesLocalization> stringLocalizer
            , UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService
            ) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }
        #endregion


        #region Handlers
        public async Task<Response<JwtAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
                return NotFound<JwtAuthResponse>();

            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (signInResult.IsCompletedSuccessfully)
            {
                var result = _authenticationService.GetJWTToken(user);
                return Success(result);
            }

            return BadRequest<JwtAuthResponse>();
        }
        #endregion
    }
}
