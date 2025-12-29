using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNameIndexToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_categories_name",
                table: "categories",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_categories_name",
                table: "categories");
        }
    }
}
