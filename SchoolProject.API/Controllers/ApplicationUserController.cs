﻿using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Domain.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserCommand command)
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

        [HttpGet(Router.UserRouting.Paginated)]
        public async Task<IActionResult> GetPaginatedUserListAsync([FromQuery] GetPaginatedUserListQuery query)
        {
            try
            {
                var response = await Mediator
                    .Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
