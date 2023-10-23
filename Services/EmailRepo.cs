using System.Net;
using System.Net.Mail;

namespace EmployeeHub_MinimalAPI.Services
{
	public class EmailRepo : IEmail
	{
		private readonly SmtpClient _smtpClient;
		private readonly IConfiguration _configuration;

        public EmailRepo(IConfiguration configuration)
        {
			_configuration = configuration;

			// Read email settings from configuration
			var smtpServer = _configuration["EmailSettings:SmtpServer"];
			var port = int.Parse(_configuration["EmailSettings:Port"]);
			var userName = _configuration["EmailSettings:UserName"];
			var password = _configuration["EmailSettings:Password"];

			_smtpClient = new SmtpClient(smtpServer)
			{
				Port = port,
				Credentials = new NetworkCredential(userName, password),
				EnableSsl = true
			};
		}

        public async Task SendEmailAsync(string to, string subject, string body)
		{
			var mailMessage = new MailMessage
			{
				From = new MailAddress("your_mail@example.com"),
				Subject = subject,
				Body = body,
				IsBodyHtml = true,
			};

			mailMessage.To.Add(to);

			try
			{
				await _smtpClient.SendMailAsync(mailMessage);
			} 
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}

		}
	}
}
