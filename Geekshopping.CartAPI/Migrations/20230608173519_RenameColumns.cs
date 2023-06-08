using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geekshopping.CartAPI.Migrations
{
    public partial class RenameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_detail_cart_header_cart_header_id",
                table: "cart_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_detail_Product_product_id",
                table: "cart_detail");

            migrationBuilder.DropIndex(
                name: "IX_cart_detail_cart_header_id",
                table: "cart_detail");

            migrationBuilder.DropIndex(
                name: "IX_cart_detail_product_id",
                table: "cart_detail");

            migrationBuilder.DropColumn(
                name: "cart_header_id",
                table: "cart_detail");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "cart_detail");

            migrationBuilder.CreateIndex(
                name: "IX_cart_detail_CartHeaderId",
                table: "cart_detail",
                column: "CartHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_cart_detail_ProductId",
                table: "cart_detail",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_detail_cart_header_CartHeaderId",
                table: "cart_detail",
                column: "CartHeaderId",
                principalTable: "cart_header",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_detail_Product_ProductId",
                table: "cart_detail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_detail_cart_header_CartHeaderId",
                table: "cart_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_detail_Product_ProductId",
                table: "cart_detail");

            migrationBuilder.DropIndex(
                name: "IX_cart_detail_CartHeaderId",
                table: "cart_detail");

            migrationBuilder.DropIndex(
                name: "IX_cart_detail_ProductId",
                table: "cart_detail");

            migrationBuilder.AddColumn<long>(
                name: "cart_header_id",
                table: "cart_detail",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "product_id",
                table: "cart_detail",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_detail_cart_header_id",
                table: "cart_detail",
                column: "cart_header_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_detail_product_id",
                table: "cart_detail",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_detail_cart_header_cart_header_id",
                table: "cart_detail",
                column: "cart_header_id",
                principalTable: "cart_header",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_detail_Product_product_id",
                table: "cart_detail",
                column: "product_id",
                principalTable: "Product",
                principalColumn: "id");
        }
    }
}
