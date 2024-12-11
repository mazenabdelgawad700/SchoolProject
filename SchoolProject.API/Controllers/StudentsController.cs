using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Domain.AppMetaData;

namespace SchoolProject.API.Controllers;

[ApiController]
//[Authorize]
public class StudentController : AppControllerBase
{
    #region Handle Functions
    [HttpGet(Router.StudentRouting.List)]
    public async Task<IActionResult> GetStudentsAsync()
    {
        try
        {
            var response = await Mediator.Send(new GetStudentsQuery());
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    [HttpGet(Router.StudentRouting.Paginated)]
    public async Task<IActionResult> GetStudentsPaginated([FromQuery] GetStudentPaginatedListQuery query)
    {
        try
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    [HttpGet(Router.StudentRouting.GetById)]
    public async Task<IActionResult> GetStudentByIdAsync([FromRoute] int id)
    {
        try
        {
            var response = await Mediator.Send(new GetStudentByIdQuery(id));
            return ResponseResult(response);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    [HttpPost(Router.StudentRouting.Create)]
    public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentCommand command)
    {
        try
        {
            var response = await Mediator.Send(command);
            return ResponseResult(response);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    [HttpPut(Router.StudentRouting.Update)]
    public async Task<IActionResult> UpdateStudentAsync([FromBody] EditStudentCommand command)
    {
        try
        {
            var response = await Mediator.Send(command);
            return ResponseResult(response);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    [HttpDelete(Router.StudentRouting.Delete)]
    public async Task<IActionResult> DeleteStudentAsync([FromRoute] int id)
    {
        try
        {
            return ResponseResult(await Mediator.Send(new DeleteStudentCommand(id)));
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    #endregion
}
