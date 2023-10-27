using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHiveOblig.Data.Migrations
{
    public partial class MigrationReviewAndMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateJoined",
                table: "Bruker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Bruker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Bruker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Bruker",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Bruker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Bruker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    GuestId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewImage1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewImage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewImage3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrukerID = table.Column<int>(type: "int", nullable: false),
                    EiendomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Review_Bruker_BrukerID",
                        column: x => x.BrukerID,
                        principalTable: "Bruker",
                        principalColumn: "BrukerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_Eiendom_EiendomID",
                        column: x => x.EiendomID,
                        principalTable: "Eiendom",
                        principalColumn: "EiendomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlist",
                columns: table => new
                {
                    WishlistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrukerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlist", x => x.WishlistId);
                    table.ForeignKey(
                        name: "FK_Wishlist_Bruker_BrukerID",
                        column: x => x.BrukerID,
                        principalTable: "Bruker",
                        principalColumn: "BrukerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistEiendom",
                columns: table => new
                {
                    WishListEiendomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    EiendomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistEiendom", x => x.WishListEiendomId);
                    table.ForeignKey(
                        name: "FK_WishlistEiendom_Eiendom_EiendomId",
                        column: x => x.EiendomId,
                        principalTable: "Eiendom",
                        principalColumn: "EiendomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistEiendom_Wishlist_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlist",
                        principalColumn: "WishlistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_BrukerID",
                table: "Review",
                column: "BrukerID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_EiendomID",
                table: "Review",
                column: "EiendomID");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_BrukerID",
                table: "Wishlist",
                column: "BrukerID");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistEiendom_EiendomId",
                table: "WishlistEiendom",
                column: "EiendomId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistEiendom_WishlistId",
                table: "WishlistEiendom",
                column: "WishlistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "WishlistEiendom");

            migrationBuilder.DropTable(
                name: "Wishlist");

            migrationBuilder.DropColumn(
                name: "DateJoined",
                table: "Bruker");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Bruker");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Bruker");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Bruker");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Bruker");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Bruker");
        }
    }
}
