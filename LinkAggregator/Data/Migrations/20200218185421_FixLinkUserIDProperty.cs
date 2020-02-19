using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkAggregator.Data.Migrations
{
    public partial class FixLinkUserIDProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_AspNetUsers_UserId",
                table: "Links");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Links",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Links_UserId",
                table: "Links",
                newName: "IX_Links_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_AspNetUsers_UserID",
                table: "Links",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_AspNetUsers_UserID",
                table: "Links");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Links",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Links_UserID",
                table: "Links",
                newName: "IX_Links_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_AspNetUsers_UserId",
                table: "Links",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
