using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleLibrary.Persistence.Migrations
{
    public partial class deactivateUserAndOtherUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BookType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Book");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BookType",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookType",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Book",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Book",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
