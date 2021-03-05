using Microsoft.EntityFrameworkCore.Migrations;

namespace SJU_EFCore.Migrations
{
    public partial class AddedAirplaneSeatingsTblToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirplaneSeatings",
                columns: table => new
                {
                    AirplaneSeatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirplaneId = table.Column<int>(type: "int", nullable: false),
                    FlightClassId = table.Column<int>(type: "int", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirplaneSeatings", x => x.AirplaneSeatingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirplaneSeatings");
        }
    }
}
