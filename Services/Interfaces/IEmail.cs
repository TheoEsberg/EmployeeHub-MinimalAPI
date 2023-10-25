
namespace EmployeeHub_MinimalAPI.Services.Interfaces
{
	public interface IEmail
	{
		Task SendEmailAsync(string to, string subject, string body);
	}
}
