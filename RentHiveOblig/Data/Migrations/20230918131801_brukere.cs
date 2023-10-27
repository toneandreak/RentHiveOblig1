using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHiveOblig.Data.Migrations
{
    public partial class brukere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Eiendom");


            migrationBuilder.CreateTable(
                name: "Eiendom",
                columns: table => new
                {
                    EiendomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Soverom = table.Column<int>(type: "int", nullable: false),
                    Bad = table.Column<int>(type: "int", nullable: false),
                    Beskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pris = table.Column<int>(type: "int", nullable: false),
                    Sted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tittel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eiendom", x => x.EiendomID);
                });

            migrationBuilder.CreateTable(
                name: "Bruker",
                columns: table => new
                {
                    BrukerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrukerNavn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrukerEpost = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bruker", x => x.BrukerID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bruker");

            migrationBuilder.DropTable(
            name: "Eiendom");

        }
    }
}
