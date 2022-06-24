using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maker_checker_v1.Migrations
{
    public partial class addCOnfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceType_Validation_validationId",
                table: "ServiceType");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationProgress_Request_requestId",
                table: "ValidationProgress");

            migrationBuilder.DropIndex(
                name: "IX_ServiceType_validationId",
                table: "ServiceType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "validationId",
                table: "ServiceType");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameColumn(
                name: "requestId",
                table: "ValidationProgress",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationProgress_requestId",
                table: "ValidationProgress",
                newName: "IX_ValidationProgress_RequestId");

            migrationBuilder.RenameColumn(
                name: "timeStamp",
                table: "Validation",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "servicesTypeId",
                table: "Validation",
                newName: "ServicesTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Rule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ValidationId = table.Column<int>(type: "INTEGER", nullable: false),
                    ValidationProgressId = table.Column<int>(type: "INTEGER", nullable: false),
                    nbr = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rule_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rule_Validation_ValidationId",
                        column: x => x.ValidationId,
                        principalTable: "Validation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rule_ValidationProgress_ValidationProgressId",
                        column: x => x.ValidationProgressId,
                        principalTable: "ValidationProgress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Validation_ServicesTypeId",
                table: "Validation",
                column: "ServicesTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rule_RoleId",
                table: "Rule",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_ValidationId",
                table: "Rule",
                column: "ValidationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_ValidationProgressId",
                table: "Rule",
                column: "ValidationProgressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Validation_ServiceType_ServicesTypeId",
                table: "Validation",
                column: "ServicesTypeId",
                principalTable: "ServiceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationProgress_Request_RequestId",
                table: "ValidationProgress",
                column: "RequestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Validation_ServiceType_ServicesTypeId",
                table: "Validation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationProgress_Request_RequestId",
                table: "ValidationProgress");

            migrationBuilder.DropTable(
                name: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Validation_ServicesTypeId",
                table: "Validation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "ValidationProgress",
                newName: "requestId");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationProgress_RequestId",
                table: "ValidationProgress",
                newName: "IX_ValidationProgress_requestId");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Validation",
                newName: "timeStamp");

            migrationBuilder.RenameColumn(
                name: "ServicesTypeId",
                table: "Validation",
                newName: "servicesTypeId");

            migrationBuilder.AddColumn<int>(
                name: "validationId",
                table: "ServiceType",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_validationId",
                table: "ServiceType",
                column: "validationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceType_Validation_validationId",
                table: "ServiceType",
                column: "validationId",
                principalTable: "Validation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationProgress_Request_requestId",
                table: "ValidationProgress",
                column: "requestId",
                principalTable: "Request",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
