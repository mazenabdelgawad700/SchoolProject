using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Domain.AppMetaData;
using SchoolProject.Domain.Enums;

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
                throw new Exception(ex.Message);
            }
        }

        [HttpGet(Router.UserRouting.Paginated)]
        [Authorize(Roles = nameof(RolesEnum.Admin))]
        public async Task<IActionResult> GetPaginatedUserListAsync([FromQuery] GetPaginatedUserListQuery query)
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

        [Authorize]
        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] GetApplicationUserByIdQuery query)
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

        [Authorize]
        [HttpPut(Router.UserRouting.Update)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateApplicationUserCommand command)
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

        [Authorize]
        [HttpDelete(Router.UserRouting.Delete)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
        {
            try
            {
                var response = await Mediator.Send(new DeleteApplicationUserCommand(id));
                return ResponseResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpPut($"{Router.UserRouting.Update}Password")]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody] ChangeApplicationUserPasswordCommand command)
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
    }
}
