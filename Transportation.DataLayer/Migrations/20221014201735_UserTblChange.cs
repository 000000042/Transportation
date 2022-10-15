using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.DataLayer.Migrations
{
    public partial class UserTblChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacePicture",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "IdentificationCard",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "FacePicture",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "IdentificationCard",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "FacePicture",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "IdentificationCard",
                table: "Admin");

            migrationBuilder.AddColumn<string>(
                name: "FacePicture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationCard",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacePicture",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdentificationCard",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FacePicture",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationCard",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacePicture",
                table: "Contractors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationCard",
                table: "Contractors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacePicture",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationCard",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
