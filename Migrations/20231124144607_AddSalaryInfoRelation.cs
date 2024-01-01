using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB6.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaryInfoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "SalaryInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaryInfo",
                table: "SalaryInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryInfo_EmployeeId",
                table: "SalaryInfo",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryInfo_Employees_EmployeeId",
                table: "SalaryInfo",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryInfo_Employees_EmployeeId",
                table: "SalaryInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaryInfo",
                table: "SalaryInfo");

            migrationBuilder.DropIndex(
                name: "IX_SalaryInfo_EmployeeId",
                table: "SalaryInfo");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "SalaryInfo");
        }
    }
}
