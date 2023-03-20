using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.ProductApi.Migrations
{
    public partial class up2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 4L, "Camisetas", "Camiste de caminhada", "urldaqui", "CAMISETA DE CAMINHADA", 69.97m },
                    { 5L, "Camping", "Barraca simples de camping", "urldaqui", "BARRACA DE CAMPING", 369.97m },
                    { 6L, "Camisetas", "Camiste de caminhada ergometrica", "urldaqui", "CAMISETA DE CAMINHADA ERGOMETRICA", 169.97m },
                    { 7L, "Camping", "Bolsa termica para camping", "urldaqui", "BOLSA TERMICA DE CAMPING", 69.97m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7L);
        }
    }
}
