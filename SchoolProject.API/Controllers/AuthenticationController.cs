using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Authentication.Command.Models;
using SchoolProject.Domain.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> AddDepartmentAsync([FromForm] SignInCommand command)
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

    }
}
