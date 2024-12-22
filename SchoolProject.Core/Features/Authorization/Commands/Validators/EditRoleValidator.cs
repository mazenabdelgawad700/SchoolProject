using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion


        #region Constructors
        public EditRoleValidator(IAuthorizationService authorizationService, IStringLocalizer<SharedResourcesLocalization> localizer)
        {
            _authorizationService = authorizationService;
            _localizer = localizer;
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

            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                .NotNull()
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty]);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(e => e.Name)
                .MustAsync(async (model, key, CancellationToken) => !await _authorizationService.IsRoleNameUsedButNotTheSameRuleAsync(key, model.Id))
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.RoleExists]);
        }
        #endregion


    }
}
