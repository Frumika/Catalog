using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Application.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWishlistItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wishlist_items");

            migrationBuilder.CreateTable(
                name: "wished_products",
                columns: table => new
                {
                    wishlist_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wished_products", x => new { x.wishlist_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_wished_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wished_products_wishlists_wishlist_id",
                        column: x => x.wishlist_id,
                        principalTable: "wishlists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wished_products_product_id",
                table: "wished_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_wished_products_wishlist_id",
                table: "wished_products",
                column: "wishlist_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wished_products");

            migrationBuilder.CreateTable(
                name: "wishlist_items",
                columns: table => new
                {
                    wishlist_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlist_items", x => new { x.wishlist_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_wishlist_items_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wishlist_items_wishlists_wishlist_id",
                        column: x => x.wishlist_id,
                        principalTable: "wishlists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wishlist_items_product_id",
                table: "wishlist_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_wishlist_items_wishlist_id",
                table: "wishlist_items",
                column: "wishlist_id");
        }
    }
}
