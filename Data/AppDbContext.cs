using EmployeeHub_MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Data
{
	public class AppDbContext : DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

		public DbSet<Employee> Employees { get; set; }
		public DbSet<LeaveRequest> LeaveRequests { get; set; }
		public DbSet<LeaveType> LeaveTypes { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Add test data for Employees
			var employees = new List<Employee>
			{
				new Employee
				{
					Id = 1,
					Name = "John Doe",
					Email = "john.doe@example.com",
					Password = "password123",
					VacationDays = 10,
					isAdmin = true
				},
				new Employee
				{
					Id = 2,
					Name = "Jane Smith",
					Email = "jane.smith@example.com",
					Password = "securepwd",
					VacationDays = 15,
					isAdmin = false
				}
                // Add more employees as needed
            };

			// Add test data for LeaveTypes
			var leaveTypes = new List<LeaveType>
			{
				new LeaveType
				{
					Id = 1,
					Name = "Vacation",
					MaxDays = 20
				},
				new LeaveType
				{
					Id = 2,
					Name = "Sick Leave",
					MaxDays = 10
				}
                // Add more leave types as needed
            };

			// Add test data for LeaveRequests
			var leaveRequests = new List<LeaveRequest>
			{
				new LeaveRequest
				{
					Id = 1,
					EmployeeId = 1, // Employee John Doe
					LeaveTypeId = 1, // Vacation
					Pending = 0, // Still Pending
					ResponseMessage = null,
					StartDate = new DateTime(2023, 10, 15),
					EndDate = new DateTime(2023, 10, 20),
					RequestDate = new DateTime(2023, 10, 10)
				},
				new LeaveRequest
				{
					Id = 2,
					EmployeeId = 2, // Employee Jane Smith
					LeaveTypeId = 2, // Sick Leave
					Pending = 1, // Approved
					ResponseMessage = "Approved",
					StartDate = new DateTime(2023, 11, 5),
					EndDate = new DateTime(2023, 11, 10),
					RequestDate = new DateTime(2023, 10, 25)
				}
				// Add more leave requests as needed
			};

			modelBuilder.Entity<Employee>().HasData(employees);
			modelBuilder.Entity<LeaveType>().HasData(leaveTypes);
			modelBuilder.Entity<LeaveRequest>().HasData(leaveRequests);
		}
	}
}
