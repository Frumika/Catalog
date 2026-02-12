using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedAtPropertyToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "reviews",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "reviews");
        }
    }
}
