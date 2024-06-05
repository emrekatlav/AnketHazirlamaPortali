using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnketHazirlamaPortali.API.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YesNoAnswer",
                table: "Answers");

            migrationBuilder.AddColumn<string>(
                name: "TextAnswer",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextAnswer",
                table: "Answers");

            migrationBuilder.AddColumn<bool>(
                name: "YesNoAnswer",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
