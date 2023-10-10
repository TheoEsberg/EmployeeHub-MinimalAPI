
using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Services;
using FluentValidation;
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

            builder.Services.AddScoped<IRepository<Employee>, EmployeeRepo>();

			builder.Services.AddAutoMapper(typeof(MappingConfig));
			builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.Run();
		}
	}
}