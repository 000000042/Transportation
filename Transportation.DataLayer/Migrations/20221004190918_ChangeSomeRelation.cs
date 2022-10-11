using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.DataLayer.Migrations
{
    public partial class ChangeSomeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CargoRequest_DriverContractRequestId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DriverContractRequestId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DriverContractRequestId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverContractRequestId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DriverContractRequestId",
                table: "Users",
                column: "DriverContractRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CargoRequest_DriverContractRequestId",
                table: "Users",
                column: "DriverContractRequestId",
                principalTable: "CargoRequest",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
