using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {

        private readonly IStudentService _studentService;

        public EditStudentValidator(IStudentService studentService)
        {
            ApplyValidationRules();
            ApplyCustomValidationRules();
            _studentService = studentService;
        }

        public void ApplyValidationRules()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("required")
                .NotNull().WithMessage("can not be Null")
                .MinimumLength(3).WithMessage("Min length is 3 characters")
                .MaximumLength(50).WithMessage("Max length is 50 characters");

            RuleFor(e => e.Address)
               .NotEmpty().WithMessage("required")
               .NotNull().WithMessage("can not be Null");

            RuleFor(e => e.Phone)
               .NotEmpty().WithMessage("is required")
               .NotNull().WithMessage("can not be Null")
               .Matches(@"(?:\+20|0020|0)?(10|11|12|15)\d{7}");

            RuleFor(e => e.DepartementId)
               .NotEmpty().WithMessage("is required")
               .NotNull().WithMessage("can not be Null");
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(e => e.Name)
                .MustAsync(
                    async (model, key, CancellationToken) =>
                    !await _studentService.IsStudentNameUsedButNotTheSameStudent(key, model.Id)
                );
        }
    }
}
