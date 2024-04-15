using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ASDFG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enterprises_Promotions_PromotionId1",
                table: "Enterprises");

            migrationBuilder.DropIndex(
                name: "IX_Enterprises_PromotionId1",
                table: "Enterprises");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Enterprises");

            migrationBuilder.DropColumn(
                name: "PromotionId1",
                table: "Enterprises");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "Enterprises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PromotionId1",
                table: "Enterprises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_PromotionId1",
                table: "Enterprises",
                column: "PromotionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Enterprises_Promotions_PromotionId1",
                table: "Enterprises",
                column: "PromotionId1",
                principalTable: "Promotions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
