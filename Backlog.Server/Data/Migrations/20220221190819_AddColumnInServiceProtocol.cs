using Microsoft.EntityFrameworkCore.Migrations;

namespace Backlog.Server.Data.Migrations
{
    public partial class AddColumnInServiceProtocol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServicePartsJson",
                table: "ServiceProtocols",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicePartsJson",
                table: "ServiceProtocols");
        }
    }
}
