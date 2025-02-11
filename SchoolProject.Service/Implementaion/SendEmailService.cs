using MimeKit;
using SchoolProject.Domain.Helpers;
using SchoolProject.Service.Abstracts;


namespace SchoolProject.Service.Implementaion
{
    public class SendEmailService : ISendEmailService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion

        #region Constructors
        public SendEmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion

        #region Actions
        public async Task<string> SendEmailAsync(string email, string message)
        {
            try
            {
                using MailKit.Net.Smtp.SmtpClient client = new();
                //client.Connect("smtp.gmail.com", 587);
                await client.ConnectAsync(_emailSettings.ClientConnectionString, _emailSettings.ClientConnectionPort, _emailSettings.ClientConnectionUseSSL);
                await client.AuthenticateAsync(_emailSettings.AuthenticationEmail, _emailSettings.AuthenticationPassword);
                BodyBuilder bodyBuilder = new()
                {
                    HtmlBody = message,
                    TextBody = _emailSettings.BodyBuilderTextBody
                };

                MimeMessage mimeMessage = new()
                {
                    Body = bodyBuilder.ToMessageBody(),
                };

                mimeMessage.From.Add(new MailboxAddress(_emailSettings.MailBoxSenderHeader, _emailSettings.AuthenticationEmail));
                mimeMessage.To.Add(new MailboxAddress(_emailSettings.MailBoxSenderHeader, email));
                mimeMessage.Subject = _emailSettings.MailBoxSubject;

                await client.SendAsync(mimeMessage);

                client.Disconnect(true);
                client.Dispose();

                return "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Fail";
            }
        }
        #endregion
    }
}
