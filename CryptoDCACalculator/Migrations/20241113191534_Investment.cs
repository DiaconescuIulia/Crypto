using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoDCACalculator.Migrations
{
    /// <inheritdoc />
    public partial class Investment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CryptoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CryptoAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CryptocurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Investments_Cryptocurrencies_CryptocurrencyID",
                        column: x => x.CryptocurrencyID,
                        principalTable: "Cryptocurrencies",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Investments_CryptocurrencyID",
                table: "Investments",
                column: "CryptocurrencyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investments");
        }
    }
}
