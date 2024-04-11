using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccountMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccount_Accounts_AccountId",
                table: "CurrentAccount");

            migrationBuilder.DropIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccount");

            migrationBuilder.RenameTable(
                name: "CurrentAccount",
                newName: "CurrentAccounts");

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationalLimit",
                table: "CurrentAccounts",
                type: "numeric(20,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthAverage",
                table: "CurrentAccounts",
                type: "numeric(20,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "CurrentAccounts",
                type: "numeric(10,5)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 300);

            migrationBuilder.CreateIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccounts_AccountId",
                table: "CurrentAccounts",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Accounts_AccountId",
                table: "CurrentAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Accounts_AccountId",
                table: "CurrentAccounts");

            migrationBuilder.DropIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrentAccounts_AccountId",
                table: "CurrentAccounts");

            migrationBuilder.RenameTable(
                name: "CurrentAccounts",
                newName: "CurrentAccount");

            migrationBuilder.AlterColumn<decimal>(
                name: "OperationalLimit",
                table: "CurrentAccount",
                type: "numeric",
                maxLength: 400,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthAverage",
                table: "CurrentAccount",
                type: "numeric",
                maxLength: 100,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(20,5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "CurrentAccount",
                type: "numeric",
                maxLength: 300,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,5)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavingAccounts_AccountId",
                table: "SavingAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccount",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccount_Accounts_AccountId",
                table: "CurrentAccount",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
