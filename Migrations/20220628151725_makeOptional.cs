using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maker_checker_v1.Migrations
{
    public partial class makeOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_ValidationProgress_ValidationProgressId",
                table: "Rule");

            migrationBuilder.AlterColumn<int>(
                name: "ValidationProgressId",
                table: "Rule",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_ValidationProgress_ValidationProgressId",
                table: "Rule",
                column: "ValidationProgressId",
                principalTable: "ValidationProgress",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_ValidationProgress_ValidationProgressId",
                table: "Rule");

            migrationBuilder.AlterColumn<int>(
                name: "ValidationProgressId",
                table: "Rule",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_ValidationProgress_ValidationProgressId",
                table: "Rule",
                column: "ValidationProgressId",
                principalTable: "ValidationProgress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
