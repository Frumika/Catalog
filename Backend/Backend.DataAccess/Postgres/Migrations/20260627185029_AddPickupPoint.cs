using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddPickupPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pickup_points",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    street_type = table.Column<int>(type: "integer", maxLength: 30, nullable: false),
                    street_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    house = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    building = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pickup_points", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_pickup_points",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    pickup_point_id = table.Column<int>(type: "integer", nullable: false),
                    selected_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    added_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_pickup_points", x => new { x.user_id, x.pickup_point_id });
                    table.ForeignKey(
                        name: "FK_user_pickup_points_pickup_points_pickup_point_id",
                        column: x => x.pickup_point_id,
                        principalTable: "pickup_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_pickup_points_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_pickup_points_pickup_point_id",
                table: "user_pickup_points",
                column: "pickup_point_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_pickup_points");

            migrationBuilder.DropTable(
                name: "pickup_points");
        }
    }
}
