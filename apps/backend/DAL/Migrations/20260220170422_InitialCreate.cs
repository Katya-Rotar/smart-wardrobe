using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    season_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_seasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    style_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_styles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "temperature_suitabilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    temperature_suitability_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_temperature_suitabilities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    profile_image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_type_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_type_categories_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_type_categories_types_type_id",
                        column: x => x.type_id,
                        principalTable: "types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clothing_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    temperature_suitability_id = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    last_worn_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clothing_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_clothing_items_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_items_temperature_suitabilities_temperature_suitab",
                        column: x => x.temperature_suitability_id,
                        principalTable: "temperature_suitabilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_items_types_type_id",
                        column: x => x.type_id,
                        principalTable: "types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_items_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "followers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    follower_id = table.Column<int>(type: "integer", nullable: false),
                    following_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_followers", x => x.id);
                    table.ForeignKey(
                        name: "fk_followers_users_follower_id",
                        column: x => x.follower_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_followers_users_following_id",
                        column: x => x.following_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "outfit_groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    group_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_groups_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    temperature_suitability_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfits", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfits_temperature_suitabilities_temperature_suitability_id",
                        column: x => x.temperature_suitability_id,
                        principalTable: "temperature_suitabilities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfits_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clothing_item_seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clothing_item_id = table.Column<int>(type: "integer", nullable: false),
                    season_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clothing_item_seasons", x => x.id);
                    table.ForeignKey(
                        name: "fk_clothing_item_seasons_clothing_items_clothing_item_id",
                        column: x => x.clothing_item_id,
                        principalTable: "clothing_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_item_seasons_seasons_season_id",
                        column: x => x.season_id,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clothing_item_styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clothing_item_id = table.Column<int>(type: "integer", nullable: false),
                    style_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clothing_item_styles", x => x.id);
                    table.ForeignKey(
                        name: "fk_clothing_item_styles_clothing_items_clothing_item_id",
                        column: x => x.clothing_item_id,
                        principalTable: "clothing_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clothing_item_styles_styles_style_id",
                        column: x => x.style_id,
                        principalTable: "styles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    dress_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    outfit_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_events_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_events_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_group_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_group_id = table.Column<int>(type: "integer", nullable: false),
                    outfit_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_group_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_group_items_outfit_groups_outfit_group_id",
                        column: x => x.outfit_group_id,
                        principalTable: "outfit_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_group_items_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    clothing_item_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_items_clothing_items_clothing_item_id",
                        column: x => x.clothing_item_id,
                        principalTable: "clothing_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_items_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_seasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    season_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_seasons", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_seasons_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_seasons_seasons_season_id",
                        column: x => x.season_id,
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    style_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_styles", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_styles_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_styles_styles_style_id",
                        column: x => x.style_id,
                        principalTable: "styles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outfit_tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outfit_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_outfit_tags_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_outfit_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    outfit_id = table.Column<int>(type: "integer", nullable: false),
                    image_url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    commenting_options = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publications", x => x.id);
                    table.ForeignKey(
                        name: "fk_publications_outfits_outfit_id",
                        column: x => x.outfit_id,
                        principalTable: "outfits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_publications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    event_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_notifications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false),
                    content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_comments_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "post_likes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_likes", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_likes_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_post_likes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publication_tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    publication_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publication_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_publication_tags_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_publication_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "saved_posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_saved_posts", x => x.id);
                    table.ForeignKey(
                        name: "fk_saved_posts_publications_publication_id",
                        column: x => x.publication_id,
                        principalTable: "publications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_saved_posts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment_likes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    comment_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_likes", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_likes_comments_comment_id",
                        column: x => x.comment_id,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_likes_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "followers",
                columns: new[] { "id", "created_at", "follower_id", "following_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2 },
                    { 2, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), 2, 1 }
                });

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
                columns: new[] { "id", "commenting_options", "image_url", "outfit_id", "user_id" },
                values: new object[] { 1, true, "https://fashionjackson.com/wp-content/uploads/2017/06/Fashion-Jackson-Everlane-White-Tshirt-Zara-Ripped-Black-Skinny-Jeans.jpg", 1, 1 });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "id", "content", "created_at", "publication_id", "user_id" },
                values: new object[,]
                {
                    { 1, "Great outfit!", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1 },
                    { 2, "Where did you get those shoes?", new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2 }
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
                columns: new[] { "id", "publication_id", "user_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
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
                columns: new[] { "id", "publication_id", "user_id" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "comment_likes",
                columns: new[] { "id", "comment_id", "user_id" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 3, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_clothing_item_seasons_clothing_item_id",
                table: "clothing_item_seasons",
                column: "clothing_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_item_seasons_season_id",
                table: "clothing_item_seasons",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_item_styles_clothing_item_id",
                table: "clothing_item_styles",
                column: "clothing_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_item_styles_style_id",
                table: "clothing_item_styles",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_items_category_id",
                table: "clothing_items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_items_temperature_suitability_id",
                table: "clothing_items",
                column: "temperature_suitability_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_items_type_id",
                table: "clothing_items",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_clothing_items_user_id",
                table: "clothing_items",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_likes_comment_id",
                table: "comment_likes",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_likes_user_id",
                table: "comment_likes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_publication_id",
                table: "comments",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_comments_user_id",
                table: "comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_outfit_id",
                table: "events",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_user_id",
                table: "events",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_followers_follower_id",
                table: "followers",
                column: "follower_id");

            migrationBuilder.CreateIndex(
                name: "ix_followers_following_id",
                table: "followers",
                column: "following_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_event_id",
                table: "notifications",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_group_items_outfit_group_id",
                table: "outfit_group_items",
                column: "outfit_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_group_items_outfit_id",
                table: "outfit_group_items",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_groups_user_id",
                table: "outfit_groups",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_items_clothing_item_id",
                table: "outfit_items",
                column: "clothing_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_items_outfit_id",
                table: "outfit_items",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_seasons_outfit_id",
                table: "outfit_seasons",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_seasons_season_id",
                table: "outfit_seasons",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_styles_outfit_id",
                table: "outfit_styles",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_styles_style_id",
                table: "outfit_styles",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_tags_outfit_id",
                table: "outfit_tags",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfit_tags_tag_id",
                table: "outfit_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfits_temperature_suitability_id",
                table: "outfits",
                column: "temperature_suitability_id");

            migrationBuilder.CreateIndex(
                name: "ix_outfits_user_id",
                table: "outfits",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_likes_publication_id",
                table: "post_likes",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_post_likes_user_id",
                table: "post_likes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_publication_tags_publication_id",
                table: "publication_tags",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_publication_tags_tag_id",
                table: "publication_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_outfit_id",
                table: "publications",
                column: "outfit_id");

            migrationBuilder.CreateIndex(
                name: "ix_publications_user_id",
                table: "publications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_saved_posts_publication_id",
                table: "saved_posts",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "ix_saved_posts_user_id",
                table: "saved_posts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_type_categories_category_id",
                table: "type_categories",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_type_categories_type_id",
                table: "type_categories",
                column: "type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clothing_item_seasons");

            migrationBuilder.DropTable(
                name: "clothing_item_styles");

            migrationBuilder.DropTable(
                name: "comment_likes");

            migrationBuilder.DropTable(
                name: "followers");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "outfit_group_items");

            migrationBuilder.DropTable(
                name: "outfit_items");

            migrationBuilder.DropTable(
                name: "outfit_seasons");

            migrationBuilder.DropTable(
                name: "outfit_styles");

            migrationBuilder.DropTable(
                name: "outfit_tags");

            migrationBuilder.DropTable(
                name: "post_likes");

            migrationBuilder.DropTable(
                name: "publication_tags");

            migrationBuilder.DropTable(
                name: "saved_posts");

            migrationBuilder.DropTable(
                name: "type_categories");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "outfit_groups");

            migrationBuilder.DropTable(
                name: "clothing_items");

            migrationBuilder.DropTable(
                name: "seasons");

            migrationBuilder.DropTable(
                name: "styles");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "types");

            migrationBuilder.DropTable(
                name: "outfits");

            migrationBuilder.DropTable(
                name: "temperature_suitabilities");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
