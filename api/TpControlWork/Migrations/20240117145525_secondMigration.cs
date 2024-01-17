using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpControlWork.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Earnings_Employees_EmployeeId",
                table: "Earnings");

            migrationBuilder.DropIndex(
                name: "IX_Earnings_EmployeeId",
                table: "Earnings");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Earnings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Earnings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Earnings_EmployeeId",
                table: "Earnings",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Earnings_Employees_EmployeeId",
                table: "Earnings",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
