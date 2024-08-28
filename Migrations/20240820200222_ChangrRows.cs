using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Memory.Migrations
{
    /// <inheritdoc />
    public partial class ChangrRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Row",
                table: "Games",
                newName: "Rows");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rows",
                table: "Games",
                newName: "Row");
        }
    }
}
