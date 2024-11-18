using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Domain.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Departments.Commands.Handlers
{
    public class DepartmentsCommandHandler : ResponseHandler,
        IRequestHandler<AddDepartmentCommand, Response<string>>,
        IRequestHandler<UpdateDepartmentCommand, Response<string>>,
        IRequestHandler<DeleteDepartmentCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructors
        public DepartmentsCommandHandler(IStringLocalizer<SharedResourcesLocalization> localizer, IMapper mapper, IDepartmentService departmentService) : base(localizer)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentMapping = _mapper.Map<Department>(request);
            var addDepartmentResult = await _departmentService.AddDepartmentAsync(departmentMapping);

            if (addDepartmentResult == "Success")
                return Created(addDepartmentResult);
            else if (addDepartmentResult == "Exist")
                return UnprocessableEntity<string>();
            else
                return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var deparmentMapping = _mapper.Map<Department>(request);
            var updateDepartmentResult = await _departmentService.UpdateDepartmentAsync(deparmentMapping);
            if (updateDepartmentResult == "Success")
                return Updated<string>();

            else
                return NotFound<string>();
        }
        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(request);
            var deleteDepartmentResult = await _departmentService.DeleteDepartmentAsync(department.DID);
            if (deleteDepartmentResult == "Success")
                return Deleted<string>();
            return NotFound<string>();
        }
        #endregion

    }
}
