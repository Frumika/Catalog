using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Application.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_token_orders_order_id",
                table: "refresh_token");

            migrationBuilder.DropIndex(
                name: "IX_refresh_token_order_id",
                table: "refresh_token");

            migrationBuilder.DropColumn(
                name: "order_id",
                table: "refresh_token");

            migrationBuilder.AddColumn<int>(
                name: "RefreshTokenId",
                table: "orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_RefreshTokenId",
                table: "orders",
                column: "RefreshTokenId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_refresh_token_RefreshTokenId",
                table: "orders",
                column: "RefreshTokenId",
                principalTable: "refresh_token",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_refresh_token_RefreshTokenId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_RefreshTokenId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "order_id",
                table: "refresh_token",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_order_id",
                table: "refresh_token",
                column: "order_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_token_orders_order_id",
                table: "refresh_token",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
