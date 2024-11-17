using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {

        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResourcesLocalization> localizer)
        {
            _studentService = studentService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        public void ApplyValidationRules()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                .MinimumLength(3).WithMessage(_localizer[LocalizationSharedResourcesKeys.MinLengthOfNameis3])
                .MaximumLength(50).WithMessage(_localizer[LocalizationSharedResourcesKeys.MaxLengthOfNameis50]);

            RuleFor(e => e.Address)
                           .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
                           .NotNull().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty]);

            RuleFor(e => e.Phone)
               .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
               .Matches(@"(?:\+20|0020|0)?(10|11|12|15)\d{7}")
               .WithMessage(_localizer[LocalizationSharedResourcesKeys.Phone]);

            RuleFor(e => e.DepartementId)
               .NotEmpty().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[LocalizationSharedResourcesKeys.NotEmpty]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(e => e.Name)
                .MustAsync(async
                    (key, CancellationToken) =>
                    !await _studentService.IsStudentNameUsed(key)
                )
                .WithMessage(_localizer[LocalizationSharedResourcesKeys.UserNameAlreadyInUse]);

            // Valdiate the departement id
        }
    }
}
