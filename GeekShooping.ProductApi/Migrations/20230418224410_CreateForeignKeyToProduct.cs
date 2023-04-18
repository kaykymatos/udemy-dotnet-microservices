using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShooping.ProductApi.Migrations
{
    public partial class CreateForeignKeyToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_Category_Id",
                table: "Product",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_Category_Id",
                table: "Product",
                column: "Category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_Category_Id",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_Category_Id",
                table: "Product");
        }
    }
}
