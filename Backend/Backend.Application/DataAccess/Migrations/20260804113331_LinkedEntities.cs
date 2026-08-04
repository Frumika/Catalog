using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Application.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class LinkedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "delivery_date",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "pickup_point_id",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_pickup_point_id",
                table: "orders",
                column: "pickup_point_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_pickup_points_pickup_point_id",
                table: "orders",
                column: "pickup_point_id",
                principalTable: "pickup_points",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_pickup_points_pickup_point_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_pickup_point_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "delivery_date",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "pickup_point_id",
                table: "orders");
        }
    }
}
