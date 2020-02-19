using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkAggregator.Data.Migrations
{
    public partial class FixLinkProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Links");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Links",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Links");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Links",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
