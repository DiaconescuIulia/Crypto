using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoDCACalculator.Migrations
{
    /// <inheritdoc />
    public partial class Investment_Decimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_Cryptocurrencies_CryptocurrencyID",
                table: "Investments");

            migrationBuilder.DropColumn(
                name: "CryptoID",
                table: "Investments");

            migrationBuilder.AlterColumn<Guid>(
                name: "CryptocurrencyID",
                table: "Investments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CryptoAmount",
                table: "Investments",
                type: "decimal(18,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_Cryptocurrencies_CryptocurrencyID",
                table: "Investments",
                column: "CryptocurrencyID",
                principalTable: "Cryptocurrencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Investments_Cryptocurrencies_CryptocurrencyID",
                table: "Investments");

            migrationBuilder.AlterColumn<Guid>(
                name: "CryptocurrencyID",
                table: "Investments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "CryptoAmount",
                table: "Investments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)");

            migrationBuilder.AddColumn<Guid>(
                name: "CryptoID",
                table: "Investments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Investments_Cryptocurrencies_CryptocurrencyID",
                table: "Investments",
                column: "CryptocurrencyID",
                principalTable: "Cryptocurrencies",
                principalColumn: "ID");
        }
    }
}
