using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion


        #region Constructors
        public AddRoleValidator(IStringLocalizer<SharedResourcesLocalization> localizer, IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationRules()
        {
            RuleFor(e => e.RoleName)
                .NotEmpty()
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                .NotNull()
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty]);
        }

        public void ApplyCustomValidationRules()
        {
            RuleFor(e => e.RoleName)
                .MustAsync(
                    async (key, CancellationToken) => !await _authorizationService.IsRoleExistAsync(key)
                )
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.RoleExists]);
        }
        #endregion

    }
}
