using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
    public class UpdateApplicationUserValidator : AbstractValidator<UpdateApplicationUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UpdateApplicationUserValidator(IStringLocalizer<SharedResourcesLocalization> localizer, UserManager<User> userManager)
        {
            _localizer = localizer;
            _userManager = userManager;
            ApplyValidationRules();
            //ApplyCustomValidationRules();
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
        }
        //private void ApplyCustomValidationRules()
        //{
        //}
        #endregion

    }
}
