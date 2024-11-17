using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Domain.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class DepartmentsController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetById)]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            try
            {
                var response = await Mediator.Send(query);
                return ResponseResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
