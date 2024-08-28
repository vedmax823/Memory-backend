using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memory.Migrations
{
    /// <inheritdoc />
    public partial class AddIsStartedAndIsFinished : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "Games");
        }
    }
}
