using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHiveOblig.Data.Migrations
{
    public partial class OppdatertBruker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Bruker_BrukerID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_BrukerConversation_Bruker_BrukerId",
                table: "BrukerConversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Bruker_SenderId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Bruker_BrukerID",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_Bruker_BrukerID",
                table: "Wishlist");

            migrationBuilder.DropTable(
                name: "Bruker");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_BrukerID",
                table: "Wishlist");

            migrationBuilder.DropIndex(
                name: "IX_Review_BrukerID",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Message_SenderId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_BrukerConversation_BrukerId",
                table: "BrukerConversation");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BrukerID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BrukerID",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "BrukerID",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "BrukerId",
                table: "BrukerConversation");

            migrationBuilder.DropColumn(
                name: "BrukerID",
                table: "Booking");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrukerID",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrukerID",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrukerId",
                table: "BrukerConversation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrukerID",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateTable(
                name: "Bruker",
                columns: table => new
                {
                    BrukerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bruker", x => x.BrukerID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_BrukerID",
                table: "Wishlist",
                column: "BrukerID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BrukerID",
                table: "Review",
                column: "BrukerID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_BrukerConversation_BrukerId",
                table: "BrukerConversation",
                column: "BrukerId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BrukerID",
                table: "Booking",
                column: "BrukerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Bruker_BrukerID",
                table: "Booking",
                column: "BrukerID",
                principalTable: "Bruker",
                principalColumn: "BrukerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BrukerConversation_Bruker_BrukerId",
                table: "BrukerConversation",
                column: "BrukerId",
                principalTable: "Bruker",
                principalColumn: "BrukerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Bruker_SenderId",
                table: "Message",
                column: "SenderId",
                principalTable: "Bruker",
                principalColumn: "BrukerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Bruker_BrukerID",
                table: "Review",
                column: "BrukerID",
                principalTable: "Bruker",
                principalColumn: "BrukerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_Bruker_BrukerID",
                table: "Wishlist",
                column: "BrukerID",
                principalTable: "Bruker",
                principalColumn: "BrukerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
