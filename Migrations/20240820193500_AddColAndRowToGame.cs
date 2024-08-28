using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memory.Migrations
{
    /// <inheritdoc />
    public partial class AddColAndRowToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cols",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cols",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Games");
        }
    }
}
