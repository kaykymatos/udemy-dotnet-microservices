using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CuponApi.Migrations
{
    public partial class SeedCuponDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cupon",
                columns: new[] { "id", "cupon_code", "discount_amount" },
                values: new object[] { 1L, "TESTECUPON", 10m });

            migrationBuilder.InsertData(
                table: "cupon",
                columns: new[] { "id", "cupon_code", "discount_amount" },
                values: new object[] { 2L, "TESTECUPON2023", 15m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cupon",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "cupon",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
