using EmployeeHub_MinimalAPI.Models.DTOs.Login;
using EmployeeHub_MinimalAPI.Services.Interfaces;
using EmployeeHub_MinimalAPI.Services.Password;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHub_MinimalAPI.Endpoints
{
	public static class Login
	{
		public static void ConfigureLogin(this WebApplication app)
		{
			// Login Endpoint
			app.MapPost("api/Login", LoginCheck)
				.WithName("Login")
				.WithTags("Login")
				.Produces(200)
				.Produces(404);
		}

		private async static Task<IResult> LoginCheck([FromServices] ILogin<Models.Employee> repository, LoginDTO loginData, PasswordHashingService passwordHashingService)
		{
			var result = await repository.Login(loginData, passwordHashingService);

			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
	}
}
