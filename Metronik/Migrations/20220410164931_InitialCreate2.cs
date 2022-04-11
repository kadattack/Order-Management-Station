using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metronik.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSerialNumbers_OrderProduct_AppOrderProductId",
                table: "AppSerialNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSerialNumbers",
                table: "AppSerialNumbers");

            migrationBuilder.RenameTable(
                name: "AppSerialNumbers",
                newName: "SerialNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_AppSerialNumbers_AppOrderProductId",
                table: "SerialNumbers",
                newName: "IX_SerialNumbers_AppOrderProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SerialNumbers",
                table: "SerialNumbers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_OrderProduct_AppOrderProductId",
                table: "SerialNumbers",
                column: "AppOrderProductId",
                principalTable: "OrderProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_OrderProduct_AppOrderProductId",
                table: "SerialNumbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SerialNumbers",
                table: "SerialNumbers");

            migrationBuilder.RenameTable(
                name: "SerialNumbers",
                newName: "AppSerialNumbers");

            migrationBuilder.RenameIndex(
                name: "IX_SerialNumbers_AppOrderProductId",
                table: "AppSerialNumbers",
                newName: "IX_AppSerialNumbers_AppOrderProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSerialNumbers",
                table: "AppSerialNumbers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSerialNumbers_OrderProduct_AppOrderProductId",
                table: "AppSerialNumbers",
                column: "AppOrderProductId",
                principalTable: "OrderProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
