using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Emails.Commands.Model;
using SchoolProject.Domain.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class EmailsController : AppControllerBase
    {
        [HttpPost(Router.EmailRouting.SendEamil)]
        public async Task<IActionResult> SignInAsync([FromForm] SendEmailCommand command)
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
