using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Departments.Commands.Validators
{
    public class AddDepartmentValidator : AbstractValidator<AddDepartmentCommand>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResourcesLocalization> _stringLocalizer;
        public AddDepartmentValidator(IDepartmentService departmentService, IStringLocalizer<SharedResourcesLocalization> stringLocalizer)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationsRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(e => e.DepartmentName)
                .NotEmpty().WithMessage(_stringLocalizer[LocalizationSharedResourcesKeys.NotEmpty])
                .MinimumLength(3).WithMessage(_stringLocalizer[LocalizationSharedResourcesKeys.MinLengthOfNameis3])
                .MaximumLength(50).WithMessage(_stringLocalizer[LocalizationSharedResourcesKeys.MaxLengthOfNameis50]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(e => e.DepartmentName)
                .MustAsync(async (key, CancellationToken) =>
                   !await _departmentService.IsDepartmentNameExsit(key)
                ).WithMessage(_stringLocalizer[LocalizationSharedResourcesKeys.ItemAlreadyExist]);
        }
    }
}
