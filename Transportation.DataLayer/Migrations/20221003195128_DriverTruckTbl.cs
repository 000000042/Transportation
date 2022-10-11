using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.DataLayer.Migrations
{
    public partial class DriverTruckTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriverTrucks",
                columns: table => new
                {
                    DT_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    TruckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverTrucks", x => x.DT_Id);
                    table.ForeignKey(
                        name: "FK_DriverTrucks_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DriverTrucks_TruckTypes_TruckId",
                        column: x => x.TruckId,
                        principalTable: "TruckTypes",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverTrucks_DriverId",
                table: "DriverTrucks",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverTrucks_TruckId",
                table: "DriverTrucks",
                column: "TruckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverTrucks");
        }
    }
}
