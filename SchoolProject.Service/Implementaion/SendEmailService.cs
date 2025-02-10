using MimeKit;
using SchoolProject.Domain.Constants;
using SchoolProject.Service.Abstracts;


namespace SchoolProject.Service.Implementaion
{
    public class SendEmailService : ISendEmailService
    {
        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Actions
        public async Task<string> SendEmailAsync(string email, string message)
        {
            try
            {
                using MailKit.Net.Smtp.SmtpClient client = new();
                client.Connect("smtp.gmail.com", 587);
                await client.AuthenticateAsync("mazenahmedsaleh206@gmail.com", EmailConstants.MailPassword);
                BodyBuilder bodyBuilder = new()
                {
                    HtmlBody = message,
                    TextBody = "This is my first email by Dot Net"
                };

                MimeMessage mimeMessage = new()
                {
                    Body = bodyBuilder.ToMessageBody(),
                };

                mimeMessage.From.Add(new MailboxAddress("Future School", "mazenahmedsaleh206@gmail.com"));
                mimeMessage.To.Add(new MailboxAddress("Future School", email));
                mimeMessage.Subject = "Welcome to future school";

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
