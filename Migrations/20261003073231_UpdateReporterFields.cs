using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAndFound.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReporterFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Items",
                newName: "ContactInfo");

            migrationBuilder.AddColumn<int>(
                name: "ReporterType",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReporterType",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Items",
                newName: "StudentId");
        }
    }
}
