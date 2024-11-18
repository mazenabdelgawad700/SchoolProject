using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Departments.Commands.Validators
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResourcesLocalization> _stringLocalizer;
        #endregion

        #region Constructors
        public UpdateDepartmentValidator(IDepartmentService departmentService, IStringLocalizer<SharedResourcesLocalization> stringLocalizer)
        {
            _departmentService = departmentService;
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handlers
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
                .MustAsync(async (model, key, CancellationToken) =>
                    !await _departmentService
                            .IsDepartmentNameUsedButNotTheSameDepartmentAsync(key, model.Id)
                );
        }
        #endregion
    }
}
