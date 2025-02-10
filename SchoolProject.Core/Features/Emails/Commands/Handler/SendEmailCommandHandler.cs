using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Emails.Commands.Model;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Emails.Commands.Handler
{
    public class SendEmailCommandHandler : ResponseHandler,
        IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        IStringLocalizer<SharedResourcesLocalization> _localizer;
        private readonly ISendEmailService _sendEmailService;
        #endregion

        #region Constructors
        public SendEmailCommandHandler(IStringLocalizer<SharedResourcesLocalization> localizer, ISendEmailService sendEmailService) : base(localizer)
        {
            _localizer = localizer;
            _sendEmailService = sendEmailService;
        }
        #endregion

        #region Actions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Message = request.Message.Trim();
                if (request.Message.Trim() is null || request.Message.Trim().Length < 15)
                {
                    return Failed<string>(_localizer[LocalizationSharedResourcesKeys.InvalidEmailMessage]);
                }
                string sendEmailResult = await _sendEmailService.SendEmailAsync(request.Email, request.Message);
                if (sendEmailResult.Equals("Success"))
                {
                    return Success<string>(_localizer[LocalizationSharedResourcesKeys.EmailSentSuccessfully]);
                }
                return Failed<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Failed<string>();
            }
        }
        #endregion
    }
}
