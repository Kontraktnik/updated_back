using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Database.Migrations
{
    public partial class UpdatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ProfileFiles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isConfirmated",
                table: "ProfileFiles",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isRequested",
                table: "ProfileFiles",
                type: "tinyint(1)",
                nullable: true);
            

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFiles_UserId",
                table: "ProfileFiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileFiles_Users_UserId",
                table: "ProfileFiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileFiles_Users_UserId",
                table: "ProfileFiles");
            
            migrationBuilder.DropIndex(
                name: "IX_ProfileFiles_UserId",
                table: "ProfileFiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProfileFiles");

            migrationBuilder.DropColumn(
                name: "isConfirmated",
                table: "ProfileFiles");

            migrationBuilder.DropColumn(
                name: "isRequested",
                table: "ProfileFiles");
        }
    }
}
