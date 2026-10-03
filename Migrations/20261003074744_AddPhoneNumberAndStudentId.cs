using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostAndFound.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberAndStudentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactInfo",
                table: "Items",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Items",
                newName: "ContactInfo");
        }
    }
}
