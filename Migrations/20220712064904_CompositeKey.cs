using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maker_checker_v1.Migrations
{
    public partial class CompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rule",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_RoleId",
                table: "Rule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rule",
                table: "Rule",
                columns: new[] { "RoleId", "ValidationId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rule",
                table: "Rule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rule",
                table: "Rule",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_RoleId",
                table: "Rule",
                column: "RoleId");
        }
    }
}
