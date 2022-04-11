using Microsoft.EntityFrameworkCore.Migrations;

namespace Backlog.Server.Data.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicePart_ServiceProtocol_ServiceProtocolId",
                table: "ServicePart");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProtocol_AspNetUsers_UserId",
                table: "ServiceProtocol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProtocol",
                table: "ServiceProtocol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicePart",
                table: "ServicePart");

            migrationBuilder.RenameTable(
                name: "ServiceProtocol",
                newName: "ServiceProtocols");

            migrationBuilder.RenameTable(
                name: "ServicePart",
                newName: "ServiceParts");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceProtocol_UserId",
                table: "ServiceProtocols",
                newName: "IX_ServiceProtocols_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicePart_ServiceProtocolId",
                table: "ServiceParts",
                newName: "IX_ServiceParts_ServiceProtocolId");

            migrationBuilder.AlterColumn<string>(
                name: "BrandModel",
                table: "ServiceProtocols",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProtocols",
                table: "ServiceProtocols",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceParts",
                table: "ServiceParts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceParts_ServiceProtocols_ServiceProtocolId",
                table: "ServiceParts",
                column: "ServiceProtocolId",
                principalTable: "ServiceProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProtocols_AspNetUsers_UserId",
                table: "ServiceProtocols",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceParts_ServiceProtocols_ServiceProtocolId",
                table: "ServiceParts");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProtocols_AspNetUsers_UserId",
                table: "ServiceProtocols");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceProtocols",
                table: "ServiceProtocols");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceParts",
                table: "ServiceParts");

            migrationBuilder.RenameTable(
                name: "ServiceProtocols",
                newName: "ServiceProtocol");

            migrationBuilder.RenameTable(
                name: "ServiceParts",
                newName: "ServicePart");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceProtocols_UserId",
                table: "ServiceProtocol",
                newName: "IX_ServiceProtocol_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceParts_ServiceProtocolId",
                table: "ServicePart",
                newName: "IX_ServicePart_ServiceProtocolId");

            migrationBuilder.AlterColumn<string>(
                name: "BrandModel",
                table: "ServiceProtocol",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceProtocol",
                table: "ServiceProtocol",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicePart",
                table: "ServicePart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePart_ServiceProtocol_ServiceProtocolId",
                table: "ServicePart",
                column: "ServiceProtocolId",
                principalTable: "ServiceProtocol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProtocol_AspNetUsers_UserId",
                table: "ServiceProtocol",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
