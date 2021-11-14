using Microsoft.EntityFrameworkCore.Migrations;

namespace ksp.crud.web.api.Migrations
{
    public partial class UpdatedBeneficiaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_Employees_EmployId",
                table: "Beneficiaries");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiaries_EmployId",
                table: "Beneficiaries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_EmployId",
                table: "Beneficiaries",
                column: "EmployId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_Employees_EmployId",
                table: "Beneficiaries",
                column: "EmployId",
                principalTable: "Employees",
                principalColumn: "EmployId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
