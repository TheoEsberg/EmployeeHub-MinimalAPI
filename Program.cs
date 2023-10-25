
using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Endpoints;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Services.Password;
using EmployeeHub_MinimalAPI.Services.Repositories;
using EmployeeHub_MinimalAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Add the database connection service
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
			
			builder.Services.AddScoped<IEmployee<Models.Employee>, EmployeeRepo>();
			builder.Services.AddScoped<ILogin<Models.Employee>, LoginRepo>();
			builder.Services.AddScoped<PasswordHashingService, PasswordHashingService>();
			builder.Services.AddScoped<ILeaveRequest<Models.LeaveRequest>, LeaveRequestRepo>();
			builder.Services.AddScoped<ILeaveType<Models.LeaveType>, LeaveTypeRepo>();
			builder.Services.AddScoped<IEmail, EmailRepo>();
			builder.Services.AddScoped<IUsedLeaveDays<Models.UsedLeaveDays>, UsedLeaveDaysRepo>();


			builder.Services.AddCors((setup =>
			{
				setup.AddPolicy("default", (options =>
				{
					options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
				}));
			}));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("default");

			app.UseHttpsRedirection();

			ConfigureEndpoinst(app);

            app.Run();
		}

		private static void ConfigureEndpoinst(WebApplication app)
		{
			app.ConfigureEmail();
			app.ConfigureEmployee();
			app.ConfigureLeaveRequest();
			app.ConfigureLeaveType();
			app.ConfigureLogin();
			app.ConfigureUsedLeaveDays();
		}
	}
}