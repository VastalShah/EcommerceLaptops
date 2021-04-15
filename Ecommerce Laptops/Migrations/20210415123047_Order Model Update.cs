using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_Laptops.Migrations
{
    public partial class OrderModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Order_ID",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Order_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Order_ID",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ID",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "ID");
        }
    }
}
