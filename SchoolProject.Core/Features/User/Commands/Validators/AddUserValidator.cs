using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.IdentityUser.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;

namespace SchoolProject.Core.Features.IdentityUser.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        #endregion

        #region Constructors
        public AddUserValidator(IStringLocalizer<SharedResourcesLocalization> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
            //ApplyCustromValidationRules();
        }
        #endregion


        #region Handlers
        private void ApplyValidationRules()
        {
            RuleFor(e => e.FullName)
               .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
               .MinimumLength(3).WithMessage(_localizer[LocalizationSharedResourcesKeys.MinLengthOfNameis3])
               .MaximumLength(50)
               .WithMessage(_localizer[LocalizationSharedResourcesKeys.MaxLengthOfNameis50]);

            RuleFor(e => e.UserName)
                .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                .MinimumLength(3)
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.MinLengthOfNameis3])
                .MaximumLength(50)
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.MaxLengthOfNameis50]);

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                .EmailAddress().WithMessage(_localizer[LocalizationSharedResourcesKeys.InvalidEmailAddress]);

            RuleFor(e => e.Password)
                .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                .Matches
                (@"""^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\[\]\{\}@#\$%&\*\^])[A-Za-z\d\[\]\{\}@#\$%&\*\^]{8,}$""gm");

            RuleFor(e => e.ConfirmPassword)
                .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                .Equal(x => x.Password)
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.MatchingPasswords]);
        }

        private void ApplyCustromValidationRules()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
