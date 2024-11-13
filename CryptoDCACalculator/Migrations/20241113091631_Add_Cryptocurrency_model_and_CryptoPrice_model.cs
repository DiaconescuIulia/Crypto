using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoDCACalculator.Migrations
{
    /// <inheritdoc />
    public partial class Add_Cryptocurrency_model_and_CryptoPrice_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cryptocurrencies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CryptoPrices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CryptocurrencyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoPrices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CryptoPrices_Cryptocurrencies_CryptocurrencyID",
                        column: x => x.CryptocurrencyID,
                        principalTable: "Cryptocurrencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoPrices_CryptocurrencyID",
                table: "CryptoPrices",
                column: "CryptocurrencyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoPrices");

            migrationBuilder.DropTable(
                name: "Cryptocurrencies");
        }
    }
}
