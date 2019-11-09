using Microsoft.EntityFrameworkCore.Migrations;

namespace Stonks.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false),
                    cash = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Portfolio_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Share",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stockId = table.Column<int>(nullable: false),
                    purchaseValue = table.Column<decimal>(nullable: false),
                    currentValue = table.Column<decimal>(nullable: false),
                    PortfoliouserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Share", x => x.id);
                    table.ForeignKey(
                        name: "FK_Share_Portfolio_PortfoliouserId",
                        column: x => x.PortfoliouserId,
                        principalTable: "Portfolio",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<decimal>(nullable: false),
                    verified = table.Column<bool>(nullable: false),
                    cash = table.Column<decimal>(nullable: false),
                    assets = table.Column<decimal>(nullable: false),
                    PortfoliouserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transaction_Portfolio_PortfoliouserId",
                        column: x => x.PortfoliouserId,
                        principalTable: "Portfolio",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Share_PortfoliouserId",
                table: "Share",
                column: "PortfoliouserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PortfoliouserId",
                table: "Transaction",
                column: "PortfoliouserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Share");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Portfolio");
        }
    }
}
