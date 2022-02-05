using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class Vote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "Vote",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "Inserted",
                table: "Vote",
                newName: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Vote",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Vote",
                newName: "Inserted");
        }
    }
}
