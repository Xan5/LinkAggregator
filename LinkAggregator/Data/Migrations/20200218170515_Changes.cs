using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkAggregator.Data.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Pluses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Links",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pluses_UserId1",
                table: "Pluses",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Links_UserId",
                table: "Links",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_AspNetUsers_UserId",
                table: "Links",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pluses_AspNetUsers_UserId1",
                table: "Pluses",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_AspNetUsers_UserId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Pluses_AspNetUsers_UserId1",
                table: "Pluses");

            migrationBuilder.DropIndex(
                name: "IX_Pluses_UserId1",
                table: "Pluses");

            migrationBuilder.DropIndex(
                name: "IX_Links_UserId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Pluses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Links");
        }
    }
}
