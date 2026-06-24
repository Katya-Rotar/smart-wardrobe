using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStructureAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 1,
                column: "type_name",
                value: "Dress");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 2,
                column: "type_name",
                value: "Tee");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 3,
                column: "type_name",
                value: "Blouse");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 4,
                column: "type_name",
                value: "Shorts");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 5,
                column: "type_name",
                value: "Tank");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 6,
                column: "type_name",
                value: "Skirt");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 7,
                column: "type_name",
                value: "Cardigan");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 8,
                column: "type_name",
                value: "Sweater");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 9,
                column: "type_name",
                value: "Jacket");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 10,
                column: "type_name",
                value: "Top");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 11,
                column: "type_name",
                value: "Blazer");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 12,
                column: "type_name",
                value: "Jeans");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 13,
                column: "type_name",
                value: "Jumpsuit");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 14,
                column: "type_name",
                value: "Leggings");

            migrationBuilder.InsertData(
                table: "types",
                columns: new[] { "id", "type_name" },
                values: new object[,]
                {
                    { 15, "Hoodie" },
                    { 16, "Sweatpants" },
                    { 17, "Coat" },
                    { 18, "Parka" },
                    { 19, "Jeggings" },
                    { 20, "Chinos" },
                    { 21, "Culottes" },
                    { 22, "Flannel" },
                    { 23, "Bomber" },
                    { 24, "Anorak" },
                    { 25, "Turtleneck" },
                    { 26, "Peacoat" },
                    { 27, "Sneakers" },
                    { 28, "Boots" },
                    { 29, "Heels" },
                    { 30, "Flats" },
                    { 31, "Sandals" },
                    { 32, "Loafers" }
                });
            
            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: 4,
                column: "category_name",
                value: "Full_body");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: 6,
                column: "category_name",
                value: "Sweaters_Hoodies");

            migrationBuilder.UpdateData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 1,
                column: "type_id",
                value: 9);

            migrationBuilder.UpdateData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 2,
                column: "type_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 3,
                column: "type_id",
                value: 12);

            migrationBuilder.UpdateData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 4,
                column: "type_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "outfits",
                keyColumn: "id",
                keyValue: 1,
                column: "temperature_suitability_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "outfits",
                keyColumn: "id",
                keyValue: 2,
                column: "temperature_suitability_id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 1,
                column: "season_name",
                value: "Demi-season");

            migrationBuilder.UpdateData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 3,
                column: "season_name",
                value: "All-season");

            migrationBuilder.UpdateData(
                table: "temperature_suitabilities",
                keyColumn: "id",
                keyValue: 1,
                column: "temperature_suitability_name",
                value: "Extra cold (-30 to -20)");

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 2, 3 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 3,
                column: "type_id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 4,
                column: "type_id",
                value: 10);

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 5,
                column: "type_id",
                value: 22);

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 2, 25 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 6, 8 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 6, 7 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 6, 15 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 1, 9 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 11,
                column: "category_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 1, 17 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 1, 18 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 1, 23 });

            migrationBuilder.InsertData(
                table: "type_categories",
                columns: new[] { "id", "category_id", "type_id" },
                values: new object[,]
                {
                    { 17, 3, 4 },
                    { 18, 3, 6 },
                    { 19, 3, 12 },
                    { 20, 3, 14 },
                    { 25, 4, 1 },
                    { 26, 4, 13 }
                });

            migrationBuilder.InsertData(
                table: "type_categories",
                columns: new[] { "id", "category_id", "type_id" },
                values: new object[,]
                {
                    { 15, 1, 24 },
                    { 16, 1, 26 },
                    { 21, 3, 16 },
                    { 22, 3, 19 },
                    { 23, 3, 20 },
                    { 24, 3, 21 },
                    { 27, 5, 27 },
                    { 28, 5, 28 },
                    { 29, 5, 29 },
                    { 30, 5, 30 },
                    { 31, 5, 31 },
                    { 32, 5, 32 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: 4,
                column: "category_name",
                value: "Dresses");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: 6,
                column: "category_name",
                value: "Accessories");

            migrationBuilder.UpdateData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 1,
                column: "type_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 2,
                column: "type_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 3,
                column: "type_id",
                value: 6);

            migrationBuilder.UpdateData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 4,
                column: "type_id",
                value: 11);

            migrationBuilder.UpdateData(
                table: "outfits",
                keyColumn: "id",
                keyValue: 1,
                column: "temperature_suitability_id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "outfits",
                keyColumn: "id",
                keyValue: 2,
                column: "temperature_suitability_id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 1,
                column: "season_name",
                value: "Spring");

            migrationBuilder.UpdateData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 3,
                column: "season_name",
                value: "Autumn");

            migrationBuilder.UpdateData(
                table: "temperature_suitabilities",
                keyColumn: "id",
                keyValue: 1,
                column: "temperature_suitability_name",
                value: "Extra could (-30 to -20)");

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 3,
                column: "type_id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 4,
                column: "type_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 5,
                column: "type_id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 3, 6 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 3, 7 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 3, 8 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 4, 9 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 4, 10 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 11,
                column: "category_id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 5, 12 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 5, 13 });

            migrationBuilder.UpdateData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "category_id", "type_id" },
                values: new object[] { 5, 14 });

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 1,
                column: "type_name",
                value: "Jacket");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 2,
                column: "type_name",
                value: "Coat");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 3,
                column: "type_name",
                value: "T-Shirt");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 4,
                column: "type_name",
                value: "Blouse");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 5,
                column: "type_name",
                value: "Sweatshirt");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 6,
                column: "type_name",
                value: "Jeans");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 7,
                column: "type_name",
                value: "Trousers");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 8,
                column: "type_name",
                value: "Shorts");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 9,
                column: "type_name",
                value: "Evening Dress");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 10,
                column: "type_name",
                value: "Casual Dress");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 11,
                column: "type_name",
                value: "Sundress");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 12,
                column: "type_name",
                value: "Sneakers");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 13,
                column: "type_name",
                value: "Boots");

            migrationBuilder.UpdateData(
                table: "types",
                keyColumn: "id",
                keyValue: 14,
                column: "type_name",
                value: "Heels");
        }
    }
}
