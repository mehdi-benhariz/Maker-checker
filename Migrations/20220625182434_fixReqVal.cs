using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maker_checker_v1.Migrations
{
    public partial class fixReqVal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidationProgressId",
                table: "Request");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValidationProgressId",
                table: "Request",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
