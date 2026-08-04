using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Application.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SomethingChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_refresh_token_RefreshTokenId",
                table: "orders");

            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "ordered_products");

            migrationBuilder.DropIndex(
                name: "IX_orders_RefreshTokenId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "total_price",
                table: "orders");

            migrationBuilder.CreateTable(
                name: "cart_positions",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_positions", x => new { x.cart_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_cart_positions_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_positions_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_positions",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    discount_percent = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)0),
                    price = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_positions", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_order_positions_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_positions_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_positions_cart_id",
                table: "cart_positions",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_positions_product_id",
                table: "cart_positions",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_positions_order_id",
                table: "order_positions",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_positions_product_id",
                table: "order_positions",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_positions");

            migrationBuilder.DropTable(
                name: "order_positions");

            migrationBuilder.AddColumn<int>(
                name: "RefreshTokenId",
                table: "orders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "total_price",
                table: "orders",
                type: "numeric(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_items", x => new { x.cart_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_cart_items_carts_cart_id",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ordered_products",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    product_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordered_products", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_ordered_products_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordered_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_RefreshTokenId",
                table: "orders",
                column: "RefreshTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_cart_id",
                table: "cart_items",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_product_id",
                table: "cart_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordered_products_order_id",
                table: "ordered_products",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordered_products_product_id",
                table: "ordered_products",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_refresh_token_RefreshTokenId",
                table: "orders",
                column: "RefreshTokenId",
                principalTable: "refresh_token",
                principalColumn: "id");
        }
    }
}
