using SendGrid;
using SendGrid.Helpers.Mail;

namespace FilmFlow.API.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<bool> SendHtmlEmail(string targetEmail, string targetName, string subject, string htmlBody)
        {
            var apiKey = _configuration.GetValue<string>("SendGrid:ApiKey");
            var fromEmailAddress = _configuration.GetValue<string>("SendGrid:FromEmail");
            var fromName = _configuration.GetValue<string>("SendGrid:FromName");

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

        public async Task<bool> SendHtmlEmailWithAttachments(string targetEmail, string targetName, string subject, string htmlBody, Dictionary<string, byte[]> attachments)
        {
            var apiKey = _configuration.GetValue<string>("SendGrid:ApiKey");
            var fromEmailAddress = _configuration.GetValue<string>("SendGrid:FromEmail");
            var fromName = _configuration.GetValue<string>("SendGrid:FromName");

            var client = new SendGridClient(apiKey);
            var message = new SendGridMessage
            {
                From = new EmailAddress(fromEmailAddress, fromName),
                Subject = subject,
                HtmlContent = htmlBody
            };
            foreach(var attachment in attachments)
            {
                message.AddAttachment($"{attachment.Key}.png", Convert.ToBase64String(attachment.Value), "image/png");
            }
            message.AddTo(new EmailAddress(targetEmail, targetName));

            var response = await client.SendEmailAsync(message);
            return response.IsSuccessStatusCode;
        }
    }
}
