using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Enterprises_EnterpriseId",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_EnterpriseId",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "PercentageOff",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Enterprises");

            migrationBuilder.RenameColumn(
                name: "StartOfPromotion",
                table: "Promotions",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "PromotionStatus",
                table: "Promotions",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "EndOfPromotion",
                table: "Promotions",
                newName: "End");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Enterprises",
                type: "text",
                nullable: false,
                defaultValue: "",
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
                name: "Address",
                table: "Enterprises",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Enterprises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PromotionEnterprises",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "integer", nullable: false),
                    EnterpriseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionEnterprises", x => new { x.PromotionId, x.EnterpriseId });
                    table.ForeignKey(
                        name: "FK_PromotionEnterprises_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionEnterprises_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionEnterprises_EnterpriseId",
                table: "PromotionEnterprises",
                column: "EnterpriseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionEnterprises");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Enterprises");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Promotions",
                newName: "StartOfPromotion");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Promotions",
                newName: "EndOfPromotion");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Promotions",
                newName: "PromotionStatus");

            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId",
                table: "Promotions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Promotions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PercentageOff",
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
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Enterprises",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Enterprises",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Enterprises",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_EnterpriseId",
                table: "Promotions",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Enterprises_EnterpriseId",
                table: "Promotions",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
