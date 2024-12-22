using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public DeleteRoleValidator(IStringLocalizer<SharedResourcesLocalization> localizer, IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Handle Functions
        private void ApplyValidationRules()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                .NotNull()
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty]);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(e => e.Id)
                .MustAsync(async (key, CancellationToken) =>
                !await _authorizationService.IsRoleHaveUsers(key))
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.RoleHasUsers]);
        }
        #endregion
    }
}
