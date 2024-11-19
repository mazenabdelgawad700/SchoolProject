using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Domain.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<bool>>
    {
        #region Fields
        //private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResourcesLocalization> localizer) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Fucntions
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapping = _mapper.Map<Student>(request);
            var addStudentResult = await _studentService.AddStudentAsync(studentMapping);

            if (addStudentResult == "Success")
                return Created(addStudentResult);

            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToUpdate = await _studentService.GetByIdAsync(request.Id);
            if (studentToUpdate is not null)
            {
                var studentMapping = _mapper.Map(request, studentToUpdate);
                var updateStudentResult = await _studentService.UpdateStudentAsync(studentMapping);

                if (updateStudentResult == "Updated")
                    return Updated<string>();
                else if (updateStudentResult == "Error")
                    return UnprocessableEntity<string>();
                else
                    return BadRequest<string>();
            }
            return NotFound<string>();
        }

        public async Task<Response<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToDelete = await _studentService.GetByIdAsync(request.StudID);
            if (studentToDelete is not null)
            {
                var studentMapping = _mapper.Map<Student>(request);
                var deleteStudent = await
                    _studentService.DeleteStudentAsync(studentMapping.StudID);

                if (deleteStudent)
                    return Deleted<bool>();
                else
                    return BadRequest<bool>();
            }
            return NotFound<bool>();
        }
        #endregion
    }
}
