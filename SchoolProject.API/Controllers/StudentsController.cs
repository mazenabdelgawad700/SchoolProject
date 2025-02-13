﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Domain.AppMetaData;
using SchoolProject.Domain.Enums;

namespace SchoolProject.API.Controllers;

[ApiController]
[Authorize(Roles = nameof(RolesEnum.Admin))]
public class StudentController : AppControllerBase
{
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
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
        }
    }

    [Authorize(Policy = nameof(ClaimsEnum.CreateStudent))]
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
            throw new Exception(ex.Message);
        }
    }

    [Authorize(Policy = nameof(ClaimsEnum.UpdateStudent))]
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
            throw new Exception(ex.Message);
        }
    }

    [Authorize(Policy = nameof(ClaimsEnum.DeleteStudent))]
    [HttpDelete(Router.StudentRouting.Delete)]
    public async Task<IActionResult> DeleteStudentAsync([FromRoute] int id)
    {
        try
        {
            return ResponseResult(await Mediator.Send(new DeleteStudentCommand(id)));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
