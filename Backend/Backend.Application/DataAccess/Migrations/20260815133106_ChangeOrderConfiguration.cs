using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Application.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delivery_date",
                table: "orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "delivery_date",
                table: "order_positions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delivery_date",
                table: "order_positions");

            migrationBuilder.AddColumn<DateTime>(
                name: "delivery_date",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
