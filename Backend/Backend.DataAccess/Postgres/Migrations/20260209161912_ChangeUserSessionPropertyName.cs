using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserSessionPropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_sessions_orders_OrderId",
                table: "user_sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_user_sessions_users_UserId",
                table: "user_sessions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_sessions",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "user_sessions",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_sessions_UserId",
                table: "user_sessions",
                newName: "IX_user_sessions_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_user_sessions_OrderId",
                table: "user_sessions",
                newName: "IX_user_sessions_order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_sessions_orders_order_id",
                table: "user_sessions",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_user_sessions_users_user_id",
                table: "user_sessions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_sessions_orders_order_id",
                table: "user_sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_user_sessions_users_user_id",
                table: "user_sessions");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_sessions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "user_sessions",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_user_sessions_user_id",
                table: "user_sessions",
                newName: "IX_user_sessions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_sessions_order_id",
                table: "user_sessions",
                newName: "IX_user_sessions_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_sessions_orders_OrderId",
                table: "user_sessions",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_user_sessions_users_UserId",
                table: "user_sessions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
