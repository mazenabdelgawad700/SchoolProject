using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {

        #region Fields
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        #endregion


        #region Constructors
        public EditStudentValidator(IStudentService studentService, IDepartmentService departmentService, IStringLocalizer<SharedResourcesLocalization> localizer)
        {
            _studentService = studentService;
            _departmentService = departmentService;
            _localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Handlers
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
                .MustAsync(
                    async (model, key, CancellationToken) =>
                    !await _studentService.IsStudentNameUsedButNotTheSameStudent(key, model.Id)
                ).WithMessage(_localizer[LocalizationSharedResourcesKeys.ItemAlreadyExist]);

            RuleFor(e => e.DepartementId)
            .MustAsync(async
                  (key, CancellationToken) =>
                  await _departmentService.IsDepartmentIdExist(key)
              )
              .WithMessage(_localizer[LocalizationSharedResourcesKeys.DepartmentIdNotFound]);
        }
        #endregion

    }
}
