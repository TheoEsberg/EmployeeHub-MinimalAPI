namespace EmployeeHub_MinimalAPI.Services
{
	public interface IEmail
	{
		Task SendEmailAsync(string to, string subject, string body);
	}
}
