using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Emails.Commands.Model;
using SchoolProject.Core.SharedResourcesHelper;

namespace SchoolProject.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        #endregion

        #region Constructors
        public SendEmailValidator(IStringLocalizer<SharedResourcesLocalization> localizer)
        {
            _localizer = localizer;
            ApplyValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.EmailMessageIsRequired])
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.InvalidEmailAddress]);

            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.EmailMessageIsRequired])
                .MinimumLength(15);
        }
        #endregion

    }
}
