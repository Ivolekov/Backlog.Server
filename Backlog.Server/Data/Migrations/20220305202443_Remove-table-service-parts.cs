using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backlog.Server.Data.Migrations
{
    public partial class Removetableserviceparts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceParts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceProtocolId = table.Column<int>(type: "int", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyPeriod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceParts_ServiceProtocols_ServiceProtocolId",
                        column: x => x.ServiceProtocolId,
                        principalTable: "ServiceProtocols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceParts_ServiceProtocolId",
                table: "ServiceParts",
                column: "ServiceProtocolId");
        }
    }
}
