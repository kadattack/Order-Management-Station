using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metronik.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    omsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    omsId = table.Column<string>(type: "TEXT", nullable: false),
                    orderId = table.Column<string>(type: "TEXT", nullable: false),
                    expectedCompletionTime = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FactoryId = table.Column<string>(type: "TEXT", nullable: false),
                    FactortyName = table.Column<string>(type: "TEXT", nullable: false),
                    FactoryAddress = table.Column<string>(type: "TEXT", nullable: false),
                    FactortyCountry = table.Column<string>(type: "TEXT", nullable: false),
                    ProductionLineId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    ProductDescription = table.Column<string>(type: "TEXT", nullable: false),
                    PoNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ExpectedStartDate = table.Column<string>(type: "TEXT", nullable: false),
                    AppOrderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_AppOrderId",
                        column: x => x.AppOrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gtin = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false),
                    Quantity = table.Column<ulong>(type: "INTEGER", nullable: false),
                    SerialNumberType = table.Column<string>(type: "TEXT", nullable: false),
                    TemplateId = table.Column<string>(type: "TEXT", nullable: false),
                    AppOrderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_AppOrderId",
                        column: x => x.AppOrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSerialNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppOrderProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSerialNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSerialNumbers_OrderProduct_AppOrderProductId",
                        column: x => x.AppOrderProductId,
                        principalTable: "OrderProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSerialNumbers_AppOrderProductId",
                table: "AppSerialNumbers",
                column: "AppOrderProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AppOrderId",
                table: "OrderDetails",
                column: "AppOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_AppOrderId",
                table: "OrderProduct",
                column: "AppOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSerialNumbers");

            migrationBuilder.DropTable(
                name: "Oms");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
