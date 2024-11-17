using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Core.Wrappers;
using SchoolProject.Domain.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Students.Queries.Handlers;

internal class StudentQueryHandler : ResponseHandler,
    IRequestHandler<GetStudentsQuery, Response<List<GetStudentsResponse>>>,
    IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>,
    IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
{
    #region Fields
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;
    #endregion

    #region Constructors
    public StudentQueryHandler(IMapper mapper, IStudentService studentService, IStringLocalizer<SharedResourcesLocalization> localizer) : base(localizer)
    {
        _studentService = studentService;
        _mapper = mapper;
    }
    #endregion

    #region Handle Funcitons
    public async Task<Response<List<GetStudentsResponse>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var studentsList = await _studentService.GetStudentsAsync();
        var studentsListMapper = _mapper.Map<List<GetStudentsResponse>>(studentsList);
        var result = Success(studentsListMapper);
        result.Meta = new { studentsListMapper.Count };
        return result;
    }

    public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        if (student is null)
            return NotFound<GetSingleStudentResponse>();
        var studentMapper = _mapper.Map<GetSingleStudentResponse>(student);
        return Success(studentMapper);
    }

    public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Student, GetStudentPaginatedListResponse>> expression =
             e => new
             GetStudentPaginatedListResponse(e.StudID, e.Name!, e.Address!, e.Department!.DName!);

        //var studentsQuerable = _studentService.GetStudentsAsQuerable();
        var studentFilterQuerable = _studentService.GetStudentFilterPaginatedQuerable(request.OrderBy, request.Search!);
        var paginatedResult = await studentFilterQuerable
                                    .Select(expression)
                                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return paginatedResult;
    }
    #endregion
}
