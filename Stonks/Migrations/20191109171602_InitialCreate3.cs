using Microsoft.EntityFrameworkCore.Migrations;

namespace Stonks.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Share_Portfolio_PortfoliouserId",
                table: "Share");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Portfolio_PortfoliouserId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_PortfoliouserId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Share_PortfoliouserId",
                table: "Share");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "PortfoliouserId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "PortfoliouserId",
                table: "Share");

            migrationBuilder.AddColumn<int>(
                name: "portfolioId",
                table: "Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "portfolioId",
                table: "Share",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Portfolio",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company = table.Column<string>(nullable: true),
                    stockCode = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    currentValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StockValueInTime",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stockId = table.Column<int>(nullable: false),
                    value = table.Column<decimal>(nullable: false),
                    timestamp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockValueInTime", x => x.id);
                    table.ForeignKey(
                        name: "FK_StockValueInTime_Stock_stockId",
                        column: x => x.stockId,
                        principalTable: "Stock",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_portfolioId",
                table: "Transaction",
                column: "portfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Share_portfolioId",
                table: "Share",
                column: "portfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_userId",
                table: "Portfolio",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockValueInTime_stockId",
                table: "StockValueInTime",
                column: "stockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Share_Portfolio_portfolioId",
                table: "Share",
                column: "portfolioId",
                principalTable: "Portfolio",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Portfolio_portfolioId",
                table: "Transaction",
                column: "portfolioId",
                principalTable: "Portfolio",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Share_Portfolio_portfolioId",
                table: "Share");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Portfolio_portfolioId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "StockValueInTime");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_portfolioId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Share_portfolioId",
                table: "Share");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio");

            migrationBuilder.DropIndex(
                name: "IX_Portfolio_userId",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "portfolioId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "portfolioId",
                table: "Share");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Portfolio");

            migrationBuilder.AddColumn<int>(
                name: "PortfoliouserId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PortfoliouserId",
                table: "Share",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PortfoliouserId",
                table: "Transaction",
                column: "PortfoliouserId");

            migrationBuilder.CreateIndex(
                name: "IX_Share_PortfoliouserId",
                table: "Share",
                column: "PortfoliouserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Share_Portfolio_PortfoliouserId",
                table: "Share",
                column: "PortfoliouserId",
                principalTable: "Portfolio",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Portfolio_PortfoliouserId",
                table: "Transaction",
                column: "PortfoliouserId",
                principalTable: "Portfolio",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
