using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCardProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BankId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CreditCardProduct_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCardProduct_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCardProduct_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCardProduct_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric(20,5)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Term = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    BankId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CreditProduct_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditProduct_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditProduct_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditProduct_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAccountProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepositAmount = table.Column<decimal>(type: "numeric(20,5)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BankId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CurrentAccountProduct_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentAccountProduct_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrentAccountProduct_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrentAccountProduct_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    CreditProductId = table.Column<int>(type: "integer", nullable: false),
                    CreditCardProductId = table.Column<int>(type: "integer", nullable: false),
                    CurrentAccountProductId = table.Column<int>(type: "integer", nullable: false),
                    BankId = table.Column<int>(type: "integer", nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => new { x.CreditProductId, x.CreditCardProductId, x.CurrentAccountProductId });
                    table.ForeignKey(
                        name: "FK_Products_Bank_BankId",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_CreditCardProduct_CreditCardProductId",
                        column: x => x.CreditCardProductId,
                        principalTable: "CreditCardProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_CreditProduct_CreditProductId",
                        column: x => x.CreditProductId,
                        principalTable: "CreditProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_CurrentAccountProduct_CurrentAccountProductId",
                        column: x => x.CurrentAccountProductId,
                        principalTable: "CurrentAccountProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardProduct_BankId",
                table: "CreditCardProduct",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardProduct_CurrencyId",
                table: "CreditCardProduct",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCardProduct_CustomerId",
                table: "CreditCardProduct",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditProduct_BankId",
                table: "CreditProduct",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditProduct_CurrencyId",
                table: "CreditProduct",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditProduct_CustomerId",
                table: "CreditProduct",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountProduct_BankId",
                table: "CurrentAccountProduct",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountProduct_CurrencyId",
                table: "CurrentAccountProduct",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentAccountProduct_CustomerId",
                table: "CurrentAccountProduct",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BankId",
                table: "Products",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreditCardProductId",
                table: "Products",
                column: "CreditCardProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrentAccountProductId",
                table: "Products",
                column: "CurrentAccountProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                table: "Products",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "CreditCardProduct");

            migrationBuilder.DropTable(
                name: "CreditProduct");

            migrationBuilder.DropTable(
                name: "CurrentAccountProduct");
        }
    }
}
