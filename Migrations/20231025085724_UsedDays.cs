using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeHub_MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class UsedDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsedLeaveDaysId",
                table: "LeaveTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsedLeaveDaysId",
                table: "Employees",
                type: "int",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "UsedLeaveDaysId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "UsedLeaveDaysId",
                value: null);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "UsedLeaveDaysId",
                value: null);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "UsedLeaveDaysId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveTypes_UsedLeaveDaysId",
                table: "LeaveTypes",
                column: "UsedLeaveDaysId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UsedLeaveDaysId",
                table: "Employees",
                column: "UsedLeaveDaysId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_UsedLeaveDays_UsedLeaveDaysId",
                table: "Employees",
                column: "UsedLeaveDaysId",
                principalTable: "UsedLeaveDays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveTypes_UsedLeaveDays_UsedLeaveDaysId",
                table: "LeaveTypes",
                column: "UsedLeaveDaysId",
                principalTable: "UsedLeaveDays",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UsedLeaveDays_UsedLeaveDaysId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveTypes_UsedLeaveDays_UsedLeaveDaysId",
                table: "LeaveTypes");

            migrationBuilder.DropTable(
                name: "UsedLeaveDays");

            migrationBuilder.DropIndex(
                name: "IX_LeaveTypes_UsedLeaveDaysId",
                table: "LeaveTypes");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UsedLeaveDaysId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UsedLeaveDaysId",
                table: "LeaveTypes");

            migrationBuilder.DropColumn(
                name: "UsedLeaveDaysId",
                table: "Employees");
        }
    }
}
