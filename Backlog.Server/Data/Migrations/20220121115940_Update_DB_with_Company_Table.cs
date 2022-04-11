using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backlog.Server.Data.Migrations
{
    public partial class Update_DB_with_Company_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyWebAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bulstat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    C_DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    C_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfile_UserId",
                table: "CompanyProfile",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyProfile");
        }
    }
}
