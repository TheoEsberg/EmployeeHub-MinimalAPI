using EmployeeHub_MinimalAPI.Models.DTOs.Email;
using EmployeeHub_MinimalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHub_MinimalAPI.Endpoints
{
	public static class Email
	{
		public static void ConfigureEmail(this WebApplication app)
		{
			app.MapPost("api/SendMail", SendEmail).WithName("SendEmail").WithTags("Post").Produces(200).Produces(400);
		}

		private async static Task<IResult> SendEmail([FromServices] IEmail emailService, EmailRequestDTO emailRequest)
		{
			try
			{
				await emailService.SendEmailAsync(emailRequest.To, emailRequest.Subject, emailRequest.Body);
				return Results.Ok(new { message = "Email sent successfully" });
			}
			catch (Exception ex)
			{
				return Results.BadRequest(new { error = "Failed to send email", message = ex.Message });
			}
		}
	}
}
