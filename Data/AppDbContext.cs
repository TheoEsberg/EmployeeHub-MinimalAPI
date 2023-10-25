using EmployeeHub_MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

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
		public DbSet<UsedLeaveDays> UsedLeaveDays { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Add new data to the database 
			modelBuilder.Entity<Employee>().HasData(
				new Employee { Id = 1, Name = "Bob Bobsson", Email = "bob@gmail.com", Password = "$2b$10$U92VNputGbz4J.z1m3ZUp.E46DdJ22KUJ72.XPvhNXktyehEpU9ha", Salt = "$2b$10$U92VNputGbz4J.z1m3ZUp.", isAdmin = false },
				new Employee { Id = 2, Name = "Jane Smith", Email = "jane@gmail.com", Password = "$2b$10$W3PETHdv.y/exsGEkTvNPegz4rYs.bZ1/9NbI73Nv53yNMoep3bNC", Salt = "$2b$10$W3PETHdv.y/exsGEkTvNPe", VacationDays = 15, isAdmin = false },
				new Employee { Id = 3, Name = "Alice Johnson", Email = "alice@gmail.com", Password = "$2b$10$5BS2ZgnUYHKPe5Uj.DUwbecByHn9vkCH5613N.Z10E3zxQpAkuxy2", Salt = "$2b$10$5BS2ZgnUYHKPe5Uj.DUwbe", VacationDays = 12, isAdmin = false },
				new Employee { Id = 4, Name = "Dick Brown", Email = "dick@gmail.com", Password = "$2b$10$AKxwczP13370BrKE4MZ.7ONBndVMMzE4Z4X7mn0qbaZSGfbrz7Oy6", Salt = "$2b$10$AKxwczP13370BrKE4MZ.7O", VacationDays = 18, isAdmin = false },
				new Employee { Id = 5, Name = "Eva Williams", Email = "eva@gmail.com", Password = "$2b$10$aN/1IRODUdyBQd29rawy5OTqcKz.vFfQD5w.g0dTDSQZjAFdyDMx.", Salt = "$2b$10$aN/1IRODUdyBQd29rawy5O", VacationDays = 14, isAdmin = false },
				new Employee { Id = 6, Name = "Administrator", Email = "admin", Password = "$2b$10$ehYSUTlNLSDfAoIC6HNd0.m1ERoBrCNSJJVga1sR6UnaZ84jZd4Hu", Salt = "$2b$10$ehYSUTlNLSDfAoIC6HNd0.", VacationDays = 0, isAdmin = true }
			);

			modelBuilder.Entity<LeaveType>().HasData(
				new LeaveType { Id = 1, Name = "Vacation", MaxDays = 20 },
				new LeaveType { Id = 2, Name = "Sick Leave", MaxDays = 10 },
				new LeaveType { Id = 3, Name = "Maternity Leave", MaxDays = 15 },
				new LeaveType { Id = 4, Name = "Paternity Leave", MaxDays = 10 },
				new LeaveType { Id = 5, Name = "Bereavement Leave", MaxDays = 5 }
			);

			modelBuilder.Entity<UsedLeaveDays>().HasData(
				new UsedLeaveDays { Id = 1, EmployeeId = 1, LeaveTypeId = 1, Days = 10 },
				new UsedLeaveDays { Id = 2, EmployeeId = 2, LeaveTypeId = 2, Days = 10 },
				new UsedLeaveDays { Id = 3, EmployeeId = 3, LeaveTypeId = 3, Days = 0 },
				new UsedLeaveDays { Id = 4, EmployeeId = 4, LeaveTypeId = 4, Days = 0 },
				new UsedLeaveDays { Id = 5, EmployeeId = 5, LeaveTypeId = 5, Days = 1 }
			);

			modelBuilder.Entity<LeaveRequest>().HasData(
				new LeaveRequest { Id = 1, EmployeeId = 1, LeaveTypeId = 1, Pending = 0, ResponseMessage = null, StartDate = new DateTime(2023, 10, 15), EndDate = new DateTime(2023, 10, 20), RequestDate = new DateTime(2023, 10, 10) },
				new LeaveRequest { Id = 2, EmployeeId = 2, LeaveTypeId = 2, Pending = 1, ResponseMessage = "Approved", StartDate = new DateTime(2023, 11, 5), EndDate = new DateTime(2023, 11, 10), RequestDate = new DateTime(2023, 10, 25) },
				new LeaveRequest { Id = 3, EmployeeId = 3, LeaveTypeId = 3, Pending = 0, ResponseMessage = null, StartDate = new DateTime(2023, 12, 1), EndDate = new DateTime(2023, 12, 31), RequestDate = new DateTime(2023, 11, 15) },
				new LeaveRequest { Id = 4, EmployeeId = 4, LeaveTypeId = 4, Pending = 1, ResponseMessage = "Approved", StartDate = new DateTime(2023, 11, 1), EndDate = new DateTime(2023, 11, 10), RequestDate = new DateTime(2023, 10, 20) },
				new LeaveRequest { Id = 5, EmployeeId = 5, LeaveTypeId = 5, Pending = 1, ResponseMessage = "Approved", StartDate = new DateTime(2023, 10, 25), EndDate = new DateTime(2023, 10, 27), RequestDate = new DateTime(2023, 10, 18) }
			);
		}
	}
}
