using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountPercent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "pickup_points",
                newName: "added_at");

            migrationBuilder.AddColumn<byte>(
                name: "discount_percent",
                table: "products",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount_percent",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "added_at",
                table: "pickup_points",
                newName: "created_at");
        }
    }
}
