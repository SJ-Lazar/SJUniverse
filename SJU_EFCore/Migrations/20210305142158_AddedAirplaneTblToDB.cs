using Microsoft.EntityFrameworkCore.Migrations;

namespace SJU_EFCore.Migrations
{
    public partial class AddedAirplaneTblToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airplanes",
                columns: table => new
                {
                    AirplaneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirplaneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplanes", x => x.AirplaneId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airplanes");
        }
    }
}
