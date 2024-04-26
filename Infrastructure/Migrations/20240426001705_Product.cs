using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductRequests",
                table: "ProductRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductRequests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "Request_pkey",
                table: "ProductRequests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Product_pkey", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRequests_ProductId",
                table: "ProductRequests",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRequests_Products_ProductId",
                table: "ProductRequests",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRequests_Products_ProductId",
                table: "ProductRequests");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "Request_pkey",
                table: "ProductRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProductRequests_ProductId",
                table: "ProductRequests");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductRequests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductRequests",
                table: "ProductRequests",
                column: "Id");
        }
    }
}
