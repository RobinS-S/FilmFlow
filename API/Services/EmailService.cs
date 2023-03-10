using SendGrid;
using SendGrid.Helpers.Mail;

namespace FilmFlow.API.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> SendHtmlEmail(string targetEmail, string targetName, string subject, string htmlBody)
        {
            var apiKey = configuration.GetValue<string>("SendGrid:ApiKey");
            var fromEmailAddress = configuration.GetValue<string>("SendGrid:FromEmail");
            var fromName = configuration.GetValue<string>("SendGrid:FromName");

            var client = new SendGridClient(apiKey);
            var message = new SendGridMessage
            {
                From = new EmailAddress(fromEmailAddress, fromName),
                Subject = subject,
                HtmlContent = htmlBody
            };
            message.AddTo(new EmailAddress(targetEmail, targetName));

            var response = await client.SendEmailAsync(message);
            return response.IsSuccessStatusCode;
        }
    }
}
