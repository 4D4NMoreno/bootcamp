using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PromotionAndEnterprises2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterprisePromotion");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Promotions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId",
                table: "Promotions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Enterprises",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Enterprises",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "Enterprises",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Enterprises",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
                name: "IX_Promotions_EnterpriseId",
                table: "Promotions",
                column: "EnterpriseId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Enterprises_EnterpriseId",
                table: "Promotions",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enterprises_Promotions_PromotionId1",
                table: "Enterprises");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Enterprises_EnterpriseId",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_EnterpriseId",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Enterprises_PromotionId1",
                table: "Enterprises");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "Enterprises");

            migrationBuilder.DropColumn(
                name: "PromotionId1",
                table: "Enterprises");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Promotions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Enterprises",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Enterprises",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "Enterprises",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Enterprises",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "EnterprisePromotion",
                columns: table => new
                {
                    EnterprisesId = table.Column<int>(type: "integer", nullable: false),
                    PromotionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterprisePromotion", x => new { x.EnterprisesId, x.PromotionsId });
                    table.ForeignKey(
                        name: "FK_EnterprisePromotion_Enterprises_EnterprisesId",
                        column: x => x.EnterprisesId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnterprisePromotion_Promotions_PromotionsId",
                        column: x => x.PromotionsId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnterprisePromotion_PromotionsId",
                table: "EnterprisePromotion",
                column: "PromotionsId");
        }
    }
}
