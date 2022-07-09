using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maker_checker_v1.Migrations
{
    public partial class changeReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Request");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Request",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Request");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Request",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
