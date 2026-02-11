using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "category_name" },
                values: new object[,]
                {
                    { 1, "Outerwear" },
                    { 2, "Tops" },
                    { 3, "Bottoms" },
                    { 4, "Dresses" },
                    { 5, "Shoes" },
                    { 6, "Accessories" }
                });

            migrationBuilder.InsertData(
                table: "seasons",
                columns: new[] { "id", "season_name" },
                values: new object[,]
                {
                    { 1, "Spring" },
                    { 2, "Summer" },
                    { 3, "Autumn" },
                    { 4, "Winter" }
                });

            migrationBuilder.InsertData(
                table: "styles",
                columns: new[] { "id", "style_name" },
                values: new object[,]
                {
                    { 1, "Casual" },
                    { 2, "Formal" },
                    { 3, "Sporty" },
                    { 4, "Party" },
                    { 5, "Vintage" }
                });

            migrationBuilder.InsertData(
                table: "tags",
                columns: new[] { "id", "tag_name" },
                values: new object[,]
                {
                    { 1, "Casual" },
                    { 2, "Formal" },
                    { 3, "Summer" },
                    { 4, "Winter" },
                    { 5, "Athletic" }
                });

            migrationBuilder.InsertData(
                table: "temperature_suitabilities",
                columns: new[] { "id", "temperature_suitability_name" },
                values: new object[,]
                {
                    { 1, "Extra could (-30 to -20)" },
                    { 2, "Very cold (-20 to -10)" },
                    { 3, "Cold (-10 to 0)" },
                    { 4, "Chilly (0 to +10)" },
                    { 5, "Warm (+10 to +20)" },
                    { 6, "Hot (+20 to +30)" }
                });

            migrationBuilder.InsertData(
                table: "types",
                columns: new[] { "id", "type_name" },
                values: new object[,]
                {
                    { 1, "Jacket" },
                    { 2, "Coat" },
                    { 3, "T-Shirt" },
                    { 4, "Blouse" },
                    { 5, "Sweatshirt" },
                    { 6, "Jeans" },
                    { 7, "Trousers" },
                    { 8, "Shorts" },
                    { 9, "Evening Dress" },
                    { 10, "Casual Dress" },
                    { 11, "Sundress" },
                    { 12, "Sneakers" },
                    { 13, "Boots" },
                    { 14, "Heels" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "email", "password_hash", "profile_image", "role", "username" },
                values: new object[,]
                {
                    { 1, "katya@gmail.com", "katyaPassword", "https://i.pinimg.com/564x/39/33/f6/3933f64de1724bb67264818810e3f2cb.jpg", "Premium", "Katya" },
                    { 2, "anna@gmail.com", "AnnaPassword", "https://cdn.expertphotography.com/wp-content/uploads/2020/08/social-media-profile-photos.jpg", "User", "Anna" },
                    { 3, "Admin@gmail.com", "Admin", null, "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "clothing_items",
                columns: new[] { "id", "category_id", "color", "image_url", "last_worn_date", "name", "temperature_suitability_id", "type_id", "user_id" },
                values: new object[,]
                {
                    { 1, 1, "Blue", "https://e7.pngegg.com/pngimages/324/140/png-clipart-blue-washed-denim-button-up-jacket-jean-jacket-denim-h-m-jeans-women-s-jackets-blue-women-accessories-thumbnail.png", new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Blue Denim Jacket", 4, 1, 1 },
                    { 2, 2, "White", "https://png.pngtree.com/png-clipart/20230930/original/pngtree-white-t-shirt-mockup-realistic-t-shirt-png-image_13020297.png", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "White T-Shirt", 5, 3, 1 },
                    { 3, 3, "Black", "https://www.pngpacks.com/uploads/data/2058/IMG_bbgqbicsmHvp.png", new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Black Jeans", 4, 6, 1 },
                    { 4, 4, "Yellow", "https://e7.pngegg.com/pngimages/826/743/png-clipart-cocktail-dress-skirt-gown-dirndl-summer-clothes-fashion-party-dress-thumbnail.png", null, "Summer Dress", 5, 11, 1 }
                });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "id", "date", "dress_code", "location", "name", "outfit_id", "user_id" },
                values: new object[] { 3, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Casual", "Park", "Birthday Picnic", null, 1 });

            migrationBuilder.InsertData(
                table: "outfit_groups",
                columns: new[] { "id", "created_at", "description", "group_name", "user_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, "Casual Summer", 1 },
                    { 2, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, "Formal Evening", 1 }
                });

            migrationBuilder.InsertData(
                table: "outfits",
                columns: new[] { "id", "temperature_suitability_id", "user_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "profiles",
                columns: new[] { "id", "bio", "profile_image", "user_id", "username" },
                values: new object[,]
                {
                    { 1, "Lover of vintage style and cozy sweaters.", "https://i.pinimg.com/564x/39/33/f6/3933f64de1724bb67264818810e3f2cb.jpg", 1, "fashionlover123" },
                    { 2, "Streetwear is my passion.", "https://cdn.expertphotography.com/wp-content/uploads/2020/08/social-media-profile-photos.jpg", 2, "Anna" }
                });

            migrationBuilder.InsertData(
                table: "type_categories",
                columns: new[] { "id", "category_id", "type_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 2, 5 },
                    { 6, 3, 6 },
                    { 7, 3, 7 },
                    { 8, 3, 8 },
                    { 9, 4, 9 },
                    { 10, 4, 10 },
                    { 11, 4, 11 },
                    { 12, 5, 12 },
                    { 13, 5, 13 },
                    { 14, 5, 14 }
                });

            migrationBuilder.InsertData(
                table: "clothing_item_seasons",
                columns: new[] { "id", "clothing_item_id", "season_id" },
                values: new object[,]
                {
                    { 1, 1, 4 },
                    { 2, 2, 2 },
                    { 3, 3, 1 },
                    { 4, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "clothing_item_styles",
                columns: new[] { "id", "clothing_item_id", "style_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 3 },
                    { 3, 3, 2 },
                    { 4, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "events",
                columns: new[] { "id", "date", "dress_code", "location", "name", "outfit_id", "user_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Formal", "Lviv", "Friend's Wedding", 1, 1 },
                    { 2, new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Business Casual", "Kyiv", "Office Party", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "followers",
                columns: new[] { "id", "created_at", "follower_id", "following_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2 },
                    { 2, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "outfit_group_items",
                columns: new[] { "id", "outfit_group_id", "outfit_id" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "outfit_items",
                columns: new[] { "id", "clothing_item_id", "outfit_id" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 3, 1 },
                    { 3, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "outfit_seasons",
                columns: new[] { "id", "outfit_id", "season_id" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "outfit_styles",
                columns: new[] { "id", "outfit_id", "style_id" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "outfit_tags",
                columns: new[] { "id", "outfit_id", "tag_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 3 },
                    { 3, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "publications",
                columns: new[] { "id", "commenting_options", "image_url", "outfit_id", "profile_id" },
                values: new object[] { 1, true, "https://fashionjackson.com/wp-content/uploads/2017/06/Fashion-Jackson-Everlane-White-Tshirt-Zara-Ripped-Black-Skinny-Jeans.jpg", 1, 1 });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "id", "content", "created_at", "profile_id", "publication_id" },
                values: new object[,]
                {
                    { 1, "Great outfit!", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 2, "Where did you get those shoes?", new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "notifications",
                columns: new[] { "id", "created_at", "event_id", "is_read", "user_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1, false, 1 },
                    { 2, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, true, 1 }
                });

            migrationBuilder.InsertData(
                table: "post_likes",
                columns: new[] { "id", "profile_id", "publication_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "publication_tags",
                columns: new[] { "id", "publication_id", "tag_id" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "saved_posts",
                columns: new[] { "id", "profile_id", "publication_id" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "comment_likes",
                columns: new[] { "id", "comment_id", "profile_id" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 3, 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "clothing_item_seasons",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "clothing_item_seasons",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "clothing_item_seasons",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "clothing_item_seasons",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "clothing_item_styles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "clothing_item_styles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "clothing_item_styles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "clothing_item_styles",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "comment_likes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "comment_likes",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "followers",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "followers",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "notifications",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "notifications",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "outfit_group_items",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "outfit_groups",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "outfit_items",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "outfit_items",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "outfit_items",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "outfit_seasons",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "outfit_seasons",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "outfit_styles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "outfit_styles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "outfit_tags",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "outfit_tags",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "outfit_tags",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "post_likes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "post_likes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "publication_tags",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "publication_tags",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "saved_posts",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "styles",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "styles",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "temperature_suitabilities",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "temperature_suitabilities",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "type_categories",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "clothing_items",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "comments",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "events",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "outfit_groups",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "seasons",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "styles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "styles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "styles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tags",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "outfits",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "profiles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "publications",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "temperature_suitabilities",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "temperature_suitabilities",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "types",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "outfits",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "profiles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "temperature_suitabilities",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "temperature_suitabilities",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
