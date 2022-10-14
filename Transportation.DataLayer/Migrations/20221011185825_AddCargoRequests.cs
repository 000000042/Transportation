using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.DataLayer.Migrations
{
    public partial class AddCargoRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequest_CargoAnnounces_AnnounceId",
                table: "CargoRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequest_Drivers_DriverId",
                table: "CargoRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractSigns_CargoRequest_RequestId",
                table: "ContractSigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CargoRequest",
                table: "CargoRequest");

            migrationBuilder.RenameTable(
                name: "CargoRequest",
                newName: "CargoRequests");

            migrationBuilder.RenameIndex(
                name: "IX_CargoRequest_DriverId",
                table: "CargoRequests",
                newName: "IX_CargoRequests_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_CargoRequest_AnnounceId",
                table: "CargoRequests",
                newName: "IX_CargoRequests_AnnounceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CargoRequests",
                table: "CargoRequests",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoRequests_CargoAnnounces_AnnounceId",
                table: "CargoRequests",
                column: "AnnounceId",
                principalTable: "CargoAnnounces",
                principalColumn: "AnnounceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CargoRequests_Drivers_DriverId",
                table: "CargoRequests",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractSigns_CargoRequests_RequestId",
                table: "ContractSigns",
                column: "RequestId",
                principalTable: "CargoRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequests_CargoAnnounces_AnnounceId",
                table: "CargoRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequests_Drivers_DriverId",
                table: "CargoRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractSigns_CargoRequests_RequestId",
                table: "ContractSigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CargoRequests",
                table: "CargoRequests");

            migrationBuilder.RenameTable(
                name: "CargoRequests",
                newName: "CargoRequest");

            migrationBuilder.RenameIndex(
                name: "IX_CargoRequests_DriverId",
                table: "CargoRequest",
                newName: "IX_CargoRequest_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_CargoRequests_AnnounceId",
                table: "CargoRequest",
                newName: "IX_CargoRequest_AnnounceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CargoRequest",
                table: "CargoRequest",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoRequest_CargoAnnounces_AnnounceId",
                table: "CargoRequest",
                column: "AnnounceId",
                principalTable: "CargoAnnounces",
                principalColumn: "AnnounceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CargoRequest_Drivers_DriverId",
                table: "CargoRequest",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractSigns_CargoRequest_RequestId",
                table: "ContractSigns",
                column: "RequestId",
                principalTable: "CargoRequest",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
