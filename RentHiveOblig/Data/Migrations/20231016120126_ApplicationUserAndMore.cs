using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHiveOblig.Data.Migrations
{
    public partial class ApplicationUserAndMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sted",
                table: "Eiendom",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Eiendom",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Eiendom",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Eiendom",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Eiendom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Eiendom",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BrukerId",
                table: "BrukerConversation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Eiendom_ApplicationUserId",
                table: "Eiendom",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eiendom_AspNetUsers_ApplicationUserId",
                table: "Eiendom",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eiendom_AspNetUsers_ApplicationUserId",
                table: "Eiendom");

            migrationBuilder.DropIndex(
                name: "IX_Eiendom_ApplicationUserId",
                table: "Eiendom");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Eiendom");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Eiendom");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Eiendom");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Eiendom");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Eiendom");

            migrationBuilder.DropColumn(
                name: "BrukerId",
                table: "BrukerConversation");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Eiendom",
                newName: "Sted");
        }
    }
}
