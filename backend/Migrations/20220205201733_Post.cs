using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class Post : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Post",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "Inserted",
                table: "Post",
                newName: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Post",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Post",
                newName: "Inserted");
        }
    }
}
