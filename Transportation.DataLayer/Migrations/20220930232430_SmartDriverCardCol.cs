using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.DataLayer.Migrations
{
    public partial class SmartDriverCardCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmartDriverCard",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmartDriverCard",
                table: "Drivers");
        }
    }
}
