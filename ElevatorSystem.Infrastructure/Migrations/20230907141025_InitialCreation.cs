using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevatorSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elevators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentFloor = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Direction = table.Column<int>(type: "int", nullable: false),
                    OccupantsCount = table.Column<int>(type: "int", nullable: false),
                    WeightLimit = table.Column<int>(type: "int", nullable: false),
                    MaintenanceCount = table.Column<int>(type: "int", nullable: false),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    RequestedDirection = table.Column<int>(type: "int", nullable: false),
                    TargetFloor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elevators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorNumber = table.Column<int>(type: "int", nullable: false),
                    WaitingOccupants = table.Column<int>(type: "int", nullable: false),
                    TotalPeopleGoingUp = table.Column<int>(type: "int", nullable: false),
                    TotalPeopleGoingDown = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elevators");

            migrationBuilder.DropTable(
                name: "Floors");
        }
    }
}
