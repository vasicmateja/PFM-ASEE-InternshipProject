using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFM_AseeInternship.Migrations
{
    public partial class initCategoryTableChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories ",
                table: "Categories ");

            migrationBuilder.RenameTable(
                name: "Categories ",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categories ");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories ",
                table: "Categories ",
                column: "CategoryId");
        }
    }
}
