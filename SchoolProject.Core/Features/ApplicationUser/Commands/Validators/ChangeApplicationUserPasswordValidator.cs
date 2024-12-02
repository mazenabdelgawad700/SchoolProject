using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
    public class ChangeApplicationUserPasswordValidator :
        AbstractValidator<ChangeApplicationUserPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        #endregion

        #region Constructors
        public ChangeApplicationUserPasswordValidator(
            IStringLocalizer<SharedResourcesLocalization> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            //ApplyCustromValidationRules();
        }
        #endregion


        #region Handlers
        public void ApplyValidationRules()
        {

            RuleFor(x => x.Id)
               .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[LocalizationSharedResourcesKeys.Required]);

            RuleFor(e => e.CurrentPassword)
                    .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                    .NotNull().WithMessage(_localizer[LocalizationSharedResourcesKeys.Required]);

            RuleFor(e => e.NewPassword)
                    .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                    .NotNull().WithMessage(_localizer[LocalizationSharedResourcesKeys.Required]);

            RuleFor(e => e.ConfirmPassword)
                    .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                    .NotNull().WithMessage(_localizer[LocalizationSharedResourcesKeys.Required])
                    .Equal(e => e.NewPassword).WithMessage(
                _localizer[LocalizationSharedResourcesKeys.MatchingPasswords]);
            // @TODO - Send an email to the user and tell him that the password has been changed
        }
        #endregion
    }
}
