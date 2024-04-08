using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccounts_Accounts_AccountId",
                table: "CurrentAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "Customer_pkey",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "CurrentAccounts",
                newName: "CurrentAccount");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentAccounts_AccountId",
                table: "CurrentAccount",
                newName: "IX_CurrentAccount_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customers",
                type: "text",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Customers",
                type: "text",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Customers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "text",
                precision: 20,
                scale: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Bank",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bank",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "Bank",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Bank",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400);

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

            migrationBuilder.AddPrimaryKey(
                name: "Customers_pkey",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccount_Accounts_AccountId",
                table: "CurrentAccount",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentAccount_Accounts_AccountId",
                table: "CurrentAccount");

            migrationBuilder.DropPrimaryKey(
                name: "Customers_pkey",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "CurrentAccount",
                newName: "CurrentAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentAccount_AccountId",
                table: "CurrentAccounts",
                newName: "IX_CurrentAccounts_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customers",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "Customers",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Customers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldPrecision: 20,
                oldScale: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Bank",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bank",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "Bank",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Bank",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

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

            migrationBuilder.AddPrimaryKey(
                name: "Customer_pkey",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentAccounts_Accounts_AccountId",
                table: "CurrentAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
