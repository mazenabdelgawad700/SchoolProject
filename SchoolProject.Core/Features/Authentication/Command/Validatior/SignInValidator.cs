using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Core.SharedResourcesHelper;

namespace SchoolProject.Core.Features.Authentication.Command.Validatior
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _stringLocalizer;
        #endregion

        #region Constructors
        public SignInValidator(IStringLocalizer<SharedResourcesLocalization> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        #endregion

        #region Fields
        public void ApplyValidationRules()
        {
            RuleFor(e => e.UserName)
                .NotEmpty().WithMessage(_stringLocalizer[LocalizationSharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[LocalizationSharedResourcesKeys.Required]);

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage(_stringLocalizer[LocalizationSharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[LocalizationSharedResourcesKeys.Required]);
        }
        #endregion

    }
}
