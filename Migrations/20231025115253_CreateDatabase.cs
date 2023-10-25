using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeHub_MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsedLeaveDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedLeaveDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VacationDays = table.Column<int>(type: "int", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsedLeaveDaysId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_UsedLeaveDays_UsedLeaveDaysId",
                        column: x => x.UsedLeaveDaysId,
                        principalTable: "UsedLeaveDays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxDays = table.Column<int>(type: "int", nullable: false),
                    UsedLeaveDaysId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveTypes_UsedLeaveDays_UsedLeaveDaysId",
                        column: x => x.UsedLeaveDaysId,
                        principalTable: "UsedLeaveDays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Pending = table.Column<int>(type: "int", nullable: false),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Name", "Password", "Salt", "UsedLeaveDaysId", "VacationDays", "isAdmin" },
                values: new object[,]
                {
                    { 1, "bob@gmail.com", "Bob Bobsson", "$2b$10$U92VNputGbz4J.z1m3ZUp.E46DdJ22KUJ72.XPvhNXktyehEpU9ha", "$2b$10$U92VNputGbz4J.z1m3ZUp.", null, 0, false },
                    { 2, "jane@gmail.com", "Jane Smith", "$2b$10$W3PETHdv.y/exsGEkTvNPegz4rYs.bZ1/9NbI73Nv53yNMoep3bNC", "$2b$10$W3PETHdv.y/exsGEkTvNPe", null, 15, false },
                    { 3, "alice@gmail.com", "Alice Johnson", "$2b$10$5BS2ZgnUYHKPe5Uj.DUwbecByHn9vkCH5613N.Z10E3zxQpAkuxy2", "$2b$10$5BS2ZgnUYHKPe5Uj.DUwbe", null, 12, false },
                    { 4, "dick@gmail.com", "Dick Brown", "$2b$10$AKxwczP13370BrKE4MZ.7ONBndVMMzE4Z4X7mn0qbaZSGfbrz7Oy6", "$2b$10$AKxwczP13370BrKE4MZ.7O", null, 18, false },
                    { 5, "eva@gmail.com", "Eva Williams", "$2b$10$aN/1IRODUdyBQd29rawy5OTqcKz.vFfQD5w.g0dTDSQZjAFdyDMx.", "$2b$10$aN/1IRODUdyBQd29rawy5O", null, 14, false },
                    { 6, "admin", "Administrator", "$2b$10$ehYSUTlNLSDfAoIC6HNd0.m1ERoBrCNSJJVga1sR6UnaZ84jZd4Hu", "$2b$10$ehYSUTlNLSDfAoIC6HNd0.", null, 0, true }
                });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "MaxDays", "Name", "UsedLeaveDaysId" },
                values: new object[,]
                {
                    { 1, 20, "Vacation", null },
                    { 2, 10, "Sick Leave", null },
                    { 3, 15, "Maternity Leave", null },
                    { 4, 10, "Paternity Leave", null },
                    { 5, 5, "Bereavement Leave", null }
                });

            migrationBuilder.InsertData(
                table: "UsedLeaveDays",
                columns: new[] { "Id", "Days", "EmployeeId", "LeaveTypeId" },
                values: new object[,]
                {
                    { 1, 10, 1, 1 },
                    { 2, 10, 2, 2 },
                    { 3, 0, 3, 3 },
                    { 4, 0, 4, 4 },
                    { 5, 1, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "LeaveRequests",
                columns: new[] { "Id", "EmployeeId", "EndDate", "LeaveTypeId", "Pending", "RequestDate", "ResponseMessage", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, new DateTime(2023, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, new DateTime(2023, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Approved", new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UsedLeaveDaysId",
                table: "Employees",
                column: "UsedLeaveDaysId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_UsedLeaveDaysId",
                table: "LeaveTypes",
                column: "UsedLeaveDaysId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "UsedLeaveDays");
        }
    }
}
