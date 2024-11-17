using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Core.Wrappers;
using SchoolProject.Domain.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers
{
    internal class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public DepartmentQueryHandler(IStringLocalizer<SharedResourcesLocalization> localizer,
            IDepartmentService departmentService, IMapper mapper, IStudentService studentService
            )
            : base(localizer)
        {
            _localizer = localizer;
            _departmentService = departmentService;
            _mapper = mapper;
            _studentService = studentService;
        }
        #endregion

        #region Handlers
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {

            var department = await _departmentService.GetDepartmentById(request.Id);

            if (department is null) return NotFound<GetDepartmentByIdResponse>();

            Expression<Func<Student, StudentResponse>> expression =
            e => new StudentResponse(e.StudID, e.Name!);

            var studentQuerable = _studentService.GetStudentsByDepartmentIDAsQuerable(request.Id);


            var paginatedList = await studentQuerable
                                      .Select(expression)
                                      .ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            var mapping = _mapper.Map<GetDepartmentByIdResponse>(department);
            mapping.StudentList = paginatedList;

            return Success(mapping);
        }
        #endregion
    }
}
