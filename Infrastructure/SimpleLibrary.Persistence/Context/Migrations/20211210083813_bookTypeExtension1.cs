using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleLibrary.Persistence.Migrations
{
    public partial class bookTypeExtension1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookType_Book_BookId",
                table: "BookType");

            migrationBuilder.DropIndex(
                name: "IX_BookType_BookId",
                table: "BookType");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookType");

            migrationBuilder.AddColumn<int>(
                name: "BookTypeId",
                table: "Book",
                type: "int(11)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookTypeId",
                table: "Book",
                column: "BookTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookType_BookTypeId",
                table: "Book",
                column: "BookTypeId",
                principalTable: "BookType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookType_BookTypeId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_BookTypeId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "BookTypeId",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "BookType",
                type: "int(11)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookType_BookId",
                table: "BookType",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookType_Book_BookId",
                table: "BookType",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
