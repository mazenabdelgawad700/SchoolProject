using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Domain.AppMetaData;
using SchoolProject.Domain.Enums;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    [Authorize(Roles = nameof(RolesEnum.Admin))]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.AddRole)]
        public async Task<IActionResult> AddRoleAsync([FromForm] AddRoleCommand command)
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
        [HttpPut(Router.AuthorizationRouting.UpdateRole)]
        public async Task<IActionResult> UpdateRoleAsync([FromForm] EditRoleCommand command)
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
        [HttpDelete(Router.AuthorizationRouting.DeleteRole)]
        public async Task<IActionResult> DeleteRoleAsync([FromForm] DeleteRoleCommand command)
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
        [HttpGet(Router.AuthorizationRouting.GetRolesList)]
        public async Task<IActionResult> GetRolesListAsync()
        {
            try
            {
                var response = await Mediator.Send(new GetRolesListQuery());
                return ResponseResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] GetRoleByIdQuery query)
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

        [HttpGet(Router.AuthorizationRouting.GetUserRolesList)]
        public async Task<IActionResult> GetManageUserRolesAsync([FromQuery] ManageUserRolesQuery query)
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
        [HttpPut(Router.AuthorizationRouting.UpdateUserRolesList)]
        public async Task<IActionResult> UpdateUserRolesAsync([FromBody] UpdateUserRolesCommand command)
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
        [HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaimsAsync([FromRoute] int userId)
        {
            try
            {
                var response = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
                return ResponseResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaimsList)]
        public async Task<IActionResult> UpdateUserClaimsAsync([FromBody] UpdateUserClaimsCommand command)
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
