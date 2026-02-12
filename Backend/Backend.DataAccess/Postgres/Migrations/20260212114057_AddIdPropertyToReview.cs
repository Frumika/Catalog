using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddIdPropertyToReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_user_id_product_id",
                table: "reviews",
                columns: new[] { "user_id", "product_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_reviews",
                table: "reviews");

            migrationBuilder.DropIndex(
                name: "IX_reviews_user_id_product_id",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "id",
                table: "reviews");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reviews",
                table: "reviews",
                columns: new[] { "user_id", "product_id" });
        }
    }
}
