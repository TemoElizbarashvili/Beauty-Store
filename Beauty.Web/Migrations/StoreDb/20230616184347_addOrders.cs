using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beauty.Web.Migrations.StoreDb
{
    public partial class addOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "ShoppingCart",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderTotal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_OrderId",
                table: "ShoppingCart",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Orders_OrderId",
                table: "ShoppingCart",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Orders_OrderId",
                table: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_OrderId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ShoppingCart");
        }
    }
}
