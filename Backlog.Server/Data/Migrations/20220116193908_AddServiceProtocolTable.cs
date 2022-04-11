using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backlog.Server.Data.Migrations
{
    public partial class AddServiceProtocolTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "ServiceProtocol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    C_DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceProtocolStatusId = table.Column<int>(type: "int", nullable: false),
                    BrandModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimTray = table.Column<bool>(type: "bit", nullable: false),
                    Charger = table.Column<bool>(type: "bit", nullable: false),
                    Bag = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnlockPass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockPattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServiceAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaultInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProtocol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProtocol_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicePart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarrantyPeriod = table.Column<int>(type: "int", nullable: false),
                    ServiceProtocolId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePart_ServiceProtocol_ServiceProtocolId",
                        column: x => x.ServiceProtocolId,
                        principalTable: "ServiceProtocol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePart_ServiceProtocolId",
                table: "ServicePart",
                column: "ServiceProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProtocol_UserId",
                table: "ServiceProtocol",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicePart");

            migrationBuilder.DropTable(
                name: "ServiceProtocol");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
