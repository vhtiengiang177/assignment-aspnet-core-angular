using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnet_core_web_api.Migrations
{
    public partial class InitialCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscontinuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Categories",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Categories", x => new { x.CategoryID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_Product_Categories_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Categories_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Lipstick" },
                    { 2, "Cushion" },
                    { 3, "Accessories" },
                    { 4, "Fashion" },
                    { 5, "Phone" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "ID", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Ho Chi Minh City", "Ofélia" },
                    { 2, "Ho Chi Minh City", "Lemonade" },
                    { 3, "Ho Chi Minh City", "Yeppi Yeppi" },
                    { 4, "Ho Chi Minh City", "Moji" },
                    { 5, "Ho Chi Minh City", "Shein" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "IsAdmin", "Password", "Username" },
                values: new object[,]
                {
                    { 1, true, "123456789", "giangvht" },
                    { 2, false, "123456789", "giangvht1" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "DiscontinuedDate", "Name", "Price", "Rating", "ReleaseDate", "SupplierID" },
                values: new object[,]
                {
                    { 1, "With the Touch of Rose matte lipstick collection from OFÉLIA made in the USA, you'll experience 14 captivating colors that glide on your lips like rose petals.", new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Touch Of Rose", 320000.0, (short)5, new DateTime(2018, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "OFÉLIA Modern Matte Lipstick goes on bold and stays moisturize for hours! Created with pure matte powder pigments, shea butter and rich emollients; this unique formula delivers full matte opacity, a truly comfortable supreme hydration, lightweight feel, and up to 8 hours of high-impact color.", new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modern Matte Lipstick", 330000.0, (short)4, new DateTime(2018, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "The collection is inspired by mixing & matching lipstick colors in makeup as well as mixing & matching outfits in fashion.", new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beauty Glasses Lip", 250000.0, (short)5, new DateTime(2018, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, null, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Perfect Couple Dual Foundation", 390000.0, (short)4, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, null, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pencil S", 30000.0, (short)2, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 9, "A dramatically more powerful camera system. A display so responsive, every interaction feels new again. The world’s fastest smartphone chip. Exceptional durability. And a huge leap in battery life.", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPhone 13 Pro Max", 45490000.0, (short)4, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 5, null, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mini Flower", 60000.0, (short)4, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 7, null, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Happy rabbit book A5", 15000.0, (short)2, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 4, null, new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "T-Shirt aespa", 120000.0, (short)3, new DateTime(2018, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductDetails",
                columns: new[] { "ProductID", "Details" },
                values: new object[,]
                {
                    { 1, "Made by Ofélia Team" },
                    { 4, "T-Shirt" }
                });

            migrationBuilder.InsertData(
                table: "Product_Categories",
                columns: new[] { "CategoryID", "ProductID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 8 },
                    { 3, 6 },
                    { 5, 9 },
                    { 3, 5 },
                    { 4, 5 },
                    { 3, 7 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Categories_ProductID",
                table: "Product_Categories",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_Categories");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
