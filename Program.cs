
using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

            //Get all employee
            app.MapGet("api/employee", async ([FromServices] IRepository<Employee> employeeRepo) => 
			{

				var result = await employeeRepo.GetAllAsync();
				return Results.Ok(result);

            }).WithName("GetAllEmployees")
            .WithTags("Get")
            .Produces(200);

			//Get one employee
			app.MapGet("api/employee{id:int}", async ([FromServices] IRepository<Employee> employeeRepo, int id) => {

				var result = await employeeRepo.GetAsync(id);

				if (result == null)
					return Results.BadRequest();

				return Results.Ok(result);

			}).WithName("GetEmployeeById")
			.WithTags("Get")
			.Produces(200)
			.Produces(404);

            //Create employee
            app.MapGet("api/employee", async ([FromServices] IRepository<Employee> employeeRepo, Employee employee) => {

				var result = await employeeRepo.CreateAsync(employee);

                if (result == null)
                    return Results.BadRequest();

                return Results.Ok(result);

            }).WithName("CreateNewEmployee")
            .WithTags("Create")
            .Produces(200)
            .Produces(404);

            //Update employee
            app.MapGet("api/employee", async ([FromServices] IRepository<Employee> employeeRepo, Employee employee) => {

                var result = await employeeRepo.UpdateAsync(employee);

                if (result == null)
                    return Results.BadRequest();

                return Results.Ok(result);

            }).WithName("UpdateEmployee")
            .WithTags("Update")
            .Produces(200)
            .Produces(404);

            //Delete employee
            app.MapGet("api/employee{id:int}", async ([FromServices] IRepository<Employee> employeeRepo, int id) => {

                var result = await employeeRepo.DeleteAsync(id);

                if (result == null)
                    return Results.BadRequest();

                return Results.Ok(result);

            }).WithName("DeleteEmployee")
            .WithTags("Delete")
            .Produces(200)
            .Produces(404);

            app.Run();
		}
	}
}