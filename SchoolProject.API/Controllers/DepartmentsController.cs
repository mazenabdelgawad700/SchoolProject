﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Domain.AppMetaData;
using SchoolProject.Domain.Enums;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
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
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = nameof(RolesEnum.Admin))]
        [HttpPost(Router.DepartmentRouting.Create)]
        public async Task<IActionResult> AddDepartmentAsync([FromQuery] AddDepartmentCommand command)
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

        [HttpGet(Router.DepartmentRouting.List)]
        public async Task<IActionResult> GetDepartmentsAsync()
        {
            try
            {
                var response = await Mediator.Send(new GetDepartmentsQuery());
                return ResponseResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = nameof(RolesEnum.Admin))]
        [HttpPut(Router.DepartmentRouting.Update)]
        public async Task<IActionResult> UpdateDepartmentAsync([FromBody] UpdateDepartmentCommand command)
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

        [Authorize(Roles = nameof(RolesEnum.Admin))]
        [HttpDelete(Router.DepartmentRouting.Delete)]
        public async Task<IActionResult> DeleteDepartmentAsync([FromRoute] int id)
        {
            try
            {
                var response = await Mediator.Send(new DeleteDepartmentCommand(id));
                return ResponseResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
