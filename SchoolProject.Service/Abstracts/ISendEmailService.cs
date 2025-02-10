namespace SchoolProject.Service.Abstracts
{
    public interface ISendEmailService
    {
        public Task<string> SendEmailAsync(string email, string message);
    }
}
