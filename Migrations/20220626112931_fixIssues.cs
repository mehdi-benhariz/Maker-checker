using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maker_checker_v1.Migrations
{
    public partial class fixIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Validation_ServiceType_ServicesTypeId",
                table: "Validation");

            migrationBuilder.RenameColumn(
                name: "ServicesTypeId",
                table: "Validation",
                newName: "ServiceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Validation_ServicesTypeId",
                table: "Validation",
                newName: "IX_Validation_ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Validation_ServiceType_ServiceTypeId",
                table: "Validation",
                column: "ServiceTypeId",
                principalTable: "ServiceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Validation_ServiceType_ServiceTypeId",
                table: "Validation");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeId",
                table: "Validation",
                newName: "ServicesTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Validation_ServiceTypeId",
                table: "Validation",
                newName: "IX_Validation_ServicesTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Validation_ServiceType_ServicesTypeId",
                table: "Validation",
                column: "ServicesTypeId",
                principalTable: "ServiceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
